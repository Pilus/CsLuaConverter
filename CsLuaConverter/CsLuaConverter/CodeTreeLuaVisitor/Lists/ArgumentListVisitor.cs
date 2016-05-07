namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArgumentListVisitor : BaseVisitor
    {
        public bool SkipOpeningParen { get; set; }

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
            var possibleMethods = providers.TypeKnowledgeRegistry.PossibleMethods;
            var argVisitings = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[this.argumentVisitors.Length];

            var steps = new Action<Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[], PossibleMethods, IProviders>[]
            {
                this.FilterOnNumberOfArgs,
                this.VisitNonLambdaArgs,
                this.FilterOnArgTypes,
                this.VisitParenLambdaVisitors,
                this.FilterOnArgTypes,
                this.FilterOnBestScore,
            };

            foreach (var step in steps)
            {
                if (possibleMethods.IsOnlyOneMethodRemaining())
                {
                    break;
                }

                step(argVisitings, possibleMethods, providers);
            }

            var selectedMethod = possibleMethods.GetOnlyRemainingMethodOrThrow();

            // Visit remaining args
            var args = selectedMethod.GetInputArgs();
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

            //selectedMethod.RegisterMethodGenericsWithAppliedTypes(argResultingTypes);

            if (!this.SkipOpeningParen)
            {
                textWriter.Write("(");
            }

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
            providers.TypeKnowledgeRegistry.PossibleMethods = possibleMethods;
        }

        private void FilterOnNumberOfArgs(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterOnNumberOfArgs(this.argumentVisitors.Length);
        }

        private void VisitNonLambdaArgs(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            for (int index = 0; index < this.argumentVisitors.Length; index++)
            {
                var argumentVisitor = this.argumentVisitors[index];
                if (argVisitings[index] != null || IsArgumentVisitorALambda(argumentVisitor))
                {
                    continue;
                }

                providers.TypeKnowledgeRegistry.CurrentType = null;
                var argTextWriter = new IndentedTextWriterWrapper(new IndentedTextWriter(new StringWriter()));
                argumentVisitor.Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter, type);
            }
        }

        private void FilterOnArgTypes(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterOnArgTypes(argVisitings.Select(av => av?.Item2).ToArray());
        }

        private void VisitParenLambdaVisitors(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            for (int index = 0; index < this.argumentVisitors.Length; index++)
            {
                var argumentVisitor = this.argumentVisitors[index];
                if (argVisitings[index] != null || !IsArgumentVisitorParenLambda(argumentVisitor))
                {
                    continue;
                }

                providers.TypeKnowledgeRegistry.CurrentType = null;
                providers.TypeKnowledgeRegistry.ExpectedType = null;
                var argTextWriter = new IndentedTextWriterWrapper(new IndentedTextWriter(new StringWriter()));
                argumentVisitor.Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter,
                    type);
            }
        }

        private void FilterOnBestScore(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterByBestScore(argVisitings.Select(av => av?.Item2).ToArray());
        }


        public void VisitOld(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            // TODO: Change into a stage approach, where being a list of actions<possibleMethods, argVisitings> to execute on, as long as there is still more than one remaining.

            var possibleMethods = providers.TypeKnowledgeRegistry.PossibleMethods;

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

            possibleMethods.FilterOnNumberOfArgs(this.argumentVisitors.Length);
            

            MethodKnowledge selectedMethod;

            if (possibleMethods.IsOnlyOneMethodRemaining())
            {
                selectedMethod = possibleMethods.GetOnlyRemainingMethodOrThrow();
            }
            else
            {
                possibleMethods.FilterOnArgTypes(argVisitings.Select(av => av?.Item2).ToArray());

                if (possibleMethods.IsOnlyOneMethodRemaining())
                {
                    selectedMethod = possibleMethods.GetOnlyRemainingMethodOrThrow();
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
                            argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter,
                                type);
                        }
                    }

                    var argTypes = argVisitings.Select(av => av.Item2).ToArray();
                    possibleMethods.FilterOnArgTypes(argTypes);

                    if (possibleMethods.IsOnlyOneMethodRemaining())
                    {
                        selectedMethod = possibleMethods.GetOnlyRemainingMethodOrThrow();
                    }
                    else
                    {
                        possibleMethods.FilterByBestScore(argTypes);
                        if (possibleMethods.IsOnlyOneMethodRemaining())
                        {
                            selectedMethod = possibleMethods.GetOnlyRemainingMethodOrThrow();
                        }
                        else
                        {
                            throw new VisitorException("Could not find any fitting match.");
                        }
                    }
                }
            }

            // Visit remaining args
            var args = selectedMethod.GetInputArgs();
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

            //selectedMethod.RegisterMethodGenericsWithAppliedTypes(argResultingTypes);

            if (!this.SkipOpeningParen)
            {
                textWriter.Write("(");
            }

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
            providers.TypeKnowledgeRegistry.PossibleMethods = possibleMethods;

            //providers.TypeKnowledgeRegistry.CurrentType = selectedMethod.ResolveGenerics(providers).GetReturnArg();


            //// Old code
            /*
            var possibleInvocationTypesWithCorrectNumberOfArgs = new TypeKnowledge[] { }; // TODO: Refactor.
            var bestTypes = this.DetermineTheBestFittingTypes(possibleInvocationTypesWithCorrectNumberOfArgs, argVisitings);

            TypeKnowledge selectedType;
            if (bestTypes == null)
            {
                throw new VisitorException("Could not find any fitting match.");
            }

            if (bestTypes.Count == 1)
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

            if (!this.SkipOpeningParen)
            { 
                textWriter.Write("(");
            }

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
            //providers.TypeKnowledgeRegistry.PossibleMethods.SelectedType = selectedType; // TODO: This might be the version that has the generics already applied.
            providers.TypeKnowledgeRegistry.CurrentType = selectedType.ResolveGenerics(providers).GetReturnArg(); */
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
                    var score = explicitArgs.ScoreForHowWellOtherTypeFitsThis(invocationArgTypes, possibleInvocationType.IsParams);

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

        private static bool IsArgumentVisitorParenLambda(IVisitor visitor)
        {
            var argVisitor = visitor as ArgumentVisitor;
            return argVisitor?.IsArgumentVisitorParenLambda() ?? false;
        }

        /*
        private TypeKnowledge[] DetermineTypeKnowledgeForArgumentInvocation(IProviders providers)
        {
            var types = providers.TypeKnowledgeRegistry.PossibleMethods?.InvocationTypesWithAppliedGenerics ?? providers.TypeKnowledgeRegistry.PossibleMethods?.InvocationTypes;

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
        } */
    }
}