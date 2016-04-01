namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using CodeTree;
    using Expression;
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
            var possibleInvocationTypes = this.DetermineTypeKnowledgeForArgumentInvocation(providers);

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

            

            TypeKnowledge bestType = null;
            int? bestScore = null;
            if (possibleInvocationTypes.Length == 1)
            {
                bestType = possibleInvocationTypes.Single();
            }
            else
            {
                var invocationArgTypes = argVisitings.Select(av => av?.Item2).ToArray();

                foreach (var possibleInvocationType in possibleInvocationTypes)
                {
                    var argsOfCandidate = possibleInvocationType.GetInputArgs();
                    var score = argsOfCandidate.ScoreForHowWellOtherTypeFitsThis(invocationArgTypes);

                    if (bestScore == null || bestScore > score)
                    {
                        bestType = possibleInvocationType;
                        bestScore = score;
                    }
                }

                throw new NotImplementedException();
            }

            var argTextWriters = argVisitings.Select(av => av?.Item1).ToArray();
            var args = bestType.GetInputArgs();

            textWriter.Write("(");
            for (int index = 0; index < argVisitings.Length; index++)
            {
                if (argTextWriters[index] != null)
                {
                    textWriter.AppendTextWriter(argTextWriters[index]);
                }
                else
                {
                    providers.TypeKnowledgeRegistry.ExpectedType = args[Math.Min(index, args.Length - 1)];
                    this.argumentVisitors[index].Visit(textWriter, providers);
                }
                
                if (index < this.argumentVisitors.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
            textWriter.Write(")");

            providers.TypeKnowledgeRegistry.ExpectedType = null;
            providers.TypeKnowledgeRegistry.CurrentType = bestType.GetReturnArg();
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

            if (types.Count() == 1)
            {
                return types;
            }

            var typesWithCorrectNumberOfArgs = types.Where(t =>
            {
                var argsCount = t.GetNumberOfInputArgs();
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