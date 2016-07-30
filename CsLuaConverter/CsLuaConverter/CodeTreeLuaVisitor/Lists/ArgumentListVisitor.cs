namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CodeTree;
    using Expression.Lambda;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArgumentListVisitor : BaseVisitor
    {
        private readonly IVisitor[] argumentVisitors;
        private int writerIndent;

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

            this.writerIndent = textWriter.Indent;

            var steps = new Action<Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[], PossibleMethods, IProviders>[]
            {
                this.FilterOnNumberOfArgs,
                this.VisitNonLambdaArgs,
                this.FilterOnArgTypes,
                this.FilterOnNumberOfArgsInLambda,
                this.VisitParenLambdaVisitors,
                this.FilterOnArgTypes,
                this.FilterOnBestScore,
                this.FilterOnSimpleLambdaReturnType,
                this.FilterPrioitizeNonExtensions,
                this.FilterPrioitizeNonParams,
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

            // Apply method generics
            if (selectedMethod.GetNumberOfMethodGenerics() > 0)
            { 
                if (possibleMethods.MethodGenerics != null)
                {
                    selectedMethod.RegisterExplicitMethodGenericsMapping(possibleMethods.MethodGenerics);
                }
                else
                {
                    var implicitGenericsSteps = new Action<Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[], PossibleMethods, IProviders>[]
                    {
                        this.VisitNonLambdaArgs,
                        this.VisitParenLambdaVisitors,
                        this.VisitRemainingArgs
                    };

                    var i = 0;

                    while (!selectedMethod.ResolveImplicitMethodGenerics(argVisitings.Select(av => av?.Item2).ToArray()))
                    {
                        if (i >= implicitGenericsSteps.Length)
                        {
                            throw new Exception("Could not resolve all implicit method generics.");
                        }

                        implicitGenericsSteps[i](argVisitings, possibleMethods, providers);

                        i++;
                    }
                }
            }

            // Visit remaining args
            this.VisitRemainingArgs(argVisitings, possibleMethods, providers);

            var argResultingTypes = argVisitings.Select(av => av.Item2).ToArray();
            var argTextWriters = argVisitings.Select(av => av.Item1).ToArray();

            //selectedMethod.RegisterMethodGenericsWithAppliedTypes(argResultingTypes);

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
                var argTextWriter = new IndentedTextWriterWrapper(new StringWriter());
                argTextWriter.Indent = this.writerIndent;
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
                var argTextWriter = new IndentedTextWriterWrapper(new StringWriter());
                argTextWriter.Indent = this.writerIndent;
                argumentVisitor.Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter,
                    type);
            }
        }

        private void FilterOnSimpleLambdaReturnType(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            var simpleLambdaType = new TypeKnowledge[argVisitings.Length];
            for (var index = 0; index < this.argumentVisitors.Length; index++)
            {
                var argumentVisitor = this.argumentVisitors[index];
                if (argumentVisitor == null || !IsArgumentVisitorALambda(argumentVisitor) || IsArgumentVisitorParenLambda(argumentVisitor))
                {
                    continue;
                }

                var inputArg =
                    possibleMethods.GetArgumentForAllMethods(index)
                        .Select(tk => tk.GetInputArgs().Single())
                        .GroupBy(tk => tk.GetTypeObject())
                        .Single()
                        .First();

                simpleLambdaType[index] = ((ArgumentVisitor) argumentVisitor).GetReturnTypeOfSimpleLambdaVisitor(providers, inputArg);
            }

            possibleMethods.FilterOnArgLambdaReturnType(simpleLambdaType);
        }

        private void VisitRemainingArgs(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            var args = possibleMethods.GetOnlyRemainingMethodOrThrow().GetInputArgs();
            for (var index = 0; index < argVisitings.Length; index++)
            {
                if (argVisitings[index] != null) continue;

                providers.TypeKnowledgeRegistry.ExpectedType = args[Math.Min(index, args.Length - 1)];
                var argTextWriter = new IndentedTextWriterWrapper(new StringWriter());
                argTextWriter.Indent = this.writerIndent;
                this.argumentVisitors[index].Visit(argTextWriter, providers);
                var type = providers.TypeKnowledgeRegistry.CurrentType;
                argVisitings[index] = new Tuple<IIndentedTextWriterWrapper, TypeKnowledge>(argTextWriter, type);
            }
        }

        private void FilterOnBestScore(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterByBestScore(argVisitings.Select(av => av?.Item2).ToArray());
        }

        private void FilterOnNumberOfArgsInLambda(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            var numOfArgs =
                this.argumentVisitors.Select(
                    v => (v is ArgumentVisitor && IsArgumentVisitorALambda(v)) ? ((ArgumentVisitor) v).GetInputArgCountOfLambda() : null).ToArray();

            possibleMethods.FilterByNumberOfLambdaArgs(numOfArgs);
        }

        private void FilterPrioitizeNonExtensions(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterPrioitizeNonExtensions();
        }

        private void FilterPrioitizeNonParams(Tuple<IIndentedTextWriterWrapper, TypeKnowledge>[] argVisitings, PossibleMethods possibleMethods, IProviders providers)
        {
            possibleMethods.FilterPrioitizeNonParams();
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
    }
}