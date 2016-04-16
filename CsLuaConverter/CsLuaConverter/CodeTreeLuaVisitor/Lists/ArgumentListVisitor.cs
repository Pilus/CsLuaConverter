namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Expression.Lambda;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArgumentListVisitor : BaseVisitor
    {
        private readonly IVisitor[] argumentVisitors;
        public ArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenParenToken);
            var visitors = new List<IVisitor>();

            for (var i = 1; i < this.Branch.Nodes.Length - 1; i = i + 2)
            {
                visitors.Add(this.CreateVisitor(i));
                this.ExpectKind(i + 1, SyntaxKind.CommaToken, SyntaxKind.CloseParenToken);
            }

            this.argumentVisitors = visitors.ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var possibleInvocationTypesWithCorrectNumberOfArgs = this.DetermineTypeKnowledgeForArgumentInvocation(providers);

            var argVisitings = this.argumentVisitors.Select(visitor =>
            {
                if (IsArgumentVisitorALambda(visitor))
                {
                    return null;
                }

                providers.TypeKnowledgeRegistry.CurrentType = null;
                var argTextWriter = textWriter.CreateTextWriterAtSameIndent();
                visitor.Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                return new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter, type);
            }).ToArray();



            var bestTypes = this.DetermineTheBestFittingTypes(possibleInvocationTypesWithCorrectNumberOfArgs, argVisitings);

            TypeKnowledge selectedType = null;
            if (bestTypes == null)
            {
                throw new VisitorException("Could not find any fitting match.");
            }
            else if (bestTypes.Count == 1)
            {
                selectedType = bestTypes[0];
            }
            else
            {
                for (int index = 0; index < argVisitings.Length; index++)
                {
                    var argVisiting = argVisitings[index];
                    if (argVisiting == null)
                    {
                        var visitor = this.argumentVisitors[index];
                        providers.TypeKnowledgeRegistry.CurrentType = null;
                        providers.TypeKnowledgeRegistry.ExpectedType = null;
                        var argTextWriter = textWriter.CreateTextWriterAtSameIndent();
                        visitor.Visit(argTextWriter, providers);
                        var type = providers.TypeKnowledgeRegistry.CurrentType;
                        argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter, type);
                    }
                }

                bestTypes = this.DetermineTheBestFittingTypes(possibleInvocationTypesWithCorrectNumberOfArgs, argVisitings);

                if (bestTypes.Count == 1)
                {
                    selectedType = bestTypes.Single();
                }
                else
                {
                    throw new VisitorException($"Could not determine invocation result. Got {bestTypes.Count} possibilities.");
                }
            }

            // Visit remaining args
            var args = selectedType.GetInputArgs();
            for (var index = 0; index < argVisitings.Length; index++)
            {
                if (argVisitings[index] != null) continue;

                providers.TypeKnowledgeRegistry.ExpectedType = args[Math.Min(index, args.Length - 1)];
                var argTextWriter = textWriter.CreateTextWriterAtSameIndent();
                this.argumentVisitors[index].Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter, type);
            }


            var argResultingTypes = argVisitings.Select(av => av.Item2).ToArray();
            var argTextWriters = argVisitings.Select(av => av.Item1).ToArray();

            selectedType.RegisterMethodGenericsWithAppliedTypes(argResultingTypes);

            textWriter.Write("(");
            for (var index = 0; index < argVisitings.Length; index++)
            {
                textWriter.AppendTextWriter(argTextWriters[index]);
                
                if (index < this.argumentVisitors.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
            textWriter.Write(")");

            providers.TypeKnowledgeRegistry.ExpectedType = null;
            providers.TypeKnowledgeRegistry.CurrentType = selectedType.ResolveGenerics(providers).GetReturnArg();
        }

        private List<TypeKnowledge> DetermineTheBestFittingTypes(TypeKnowledge[] possibleInvocationTypes, Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings)
        {
            if (possibleInvocationTypes.Length == 1)
            {
                return possibleInvocationTypes.ToList();
            }

            // Filter away types that fits a lambda, but where the number of input args does not match.
            var invocationTypesFittingLambdas = new List<TypeKnowledge>(possibleInvocationTypes);
            for (var index = 0; index < argVisitings.Length; index++)
            {
                var argVisitor = this.argumentVisitors[index] as ArgumentVisitor;
                if (argVisitor?.IsArgumentVisitorALambda() == false || argVisitings[index] != null) continue;

                var removeableTypes = invocationTypesFittingLambdas.Where(t =>
                {
                    var args = t.GetInputArgs()[index];
                    return args.GetInputArgs().Length != argVisitor.GetInputArgCountOfLambda();
                }).ToArray();

                foreach (var removeableType in removeableTypes)
                {
                    invocationTypesFittingLambdas.Remove(removeableType);
                }
            }


            List<TypeKnowledge> bestTypes = null;
            int? bestScore = null;
            if (invocationTypesFittingLambdas.Count == 1)
            {
                bestTypes = invocationTypesFittingLambdas.ToList();
            }
            else
            {
                var invocationArgTypes = argVisitings.Select(av => av?.Item2).ToArray();

                foreach (var possibleInvocationType in invocationTypesFittingLambdas)
                {
                    var argsOfCandidate = possibleInvocationType.GetInputArgs();
                    var explicitArgs = argsOfCandidate.ApplyImplicitAndGenericTypes(invocationArgTypes);
                    var score = explicitArgs.ScoreForHowWellOtherTypeFitsThis(invocationArgTypes);

                    if (score == null) continue;
                    if (bestScore == null || bestScore > score)
                    {
                        bestTypes = new List<TypeKnowledge>() { possibleInvocationType };
                        bestScore = score;
                    }
                    else if (bestScore == score)
                    {
                        bestTypes.Add(possibleInvocationType);
                    }
                }
            }

            return bestTypes;
        }

        private static bool IsArgumentVisitorALambda(IVisitor visitor)
        {
            var argVisitor = visitor as ArgumentVisitor;
            return argVisitor?.IsArgumentVisitorALambda() ?? false;
        }

        private TypeKnowledge[] DetermineTypeKnowledgeForArgumentInvocation(IProviders providers)
        {
            var types = providers.TypeKnowledgeRegistry.PossibleMethods;

            if (types == null)
            {
                types = new [] { providers.TypeKnowledgeRegistry.CurrentType };
            }

            types = types.Where(t => t.IsDelegate()).ToArray();

            if (types.Count() == 1)
            {
                return types;
            }

            var typesWithCorrectNumberOfArgs = types.Where(t =>
            {
                var argsCount = t.GetInputArgs().Length;
                return argsCount == this.argumentVisitors.Length ||
                       (t.IsParams && argsCount <= this.argumentVisitors.Length);
            }).ToArray();

            if (!typesWithCorrectNumberOfArgs.Any())
            {
                throw new VisitorException($"Could not find a method matching the number of args: {this.argumentVisitors.Length}.");
            }

            return typesWithCorrectNumberOfArgs;
        }
    }
}