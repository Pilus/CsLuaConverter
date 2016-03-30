namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CodeTree;
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

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            var invocationType = this.DetermineTypeKnowledgeForArgumentInvocation(providers);
            var args = invocationType.GetInputArgs();

            if (args.Length != this.argumentVisitors.Length && !(invocationType.IsParams && args.Length <= this.argumentVisitors.Length))
            {
                throw new VisitorException($"Non matching number of arguments. Expected invocation with {args.Length} args. Got {this.argumentVisitors.Length} args.");
            }

            textWriter.Write("(");
            for (int index = 0; index < this.argumentVisitors.Length; index++)
            {
                var argumentVisitor = this.argumentVisitors[index];
                var arg = args[Math.Min(index, args.Length - 1)];
                providers.TypeKnowledgeRegistry.ExpectedType = arg;
                argumentVisitor.Visit(textWriter, providers);

                if (index < this.argumentVisitors.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
            textWriter.Write(")");

            providers.TypeKnowledgeRegistry.ExpectedType = null;
            providers.TypeKnowledgeRegistry.CurrentType = invocationType.GetReturnArg();
        }

        private TypeKnowledge DetermineTypeKnowledgeForArgumentInvocation(IProviders providers)
        {
            var type = providers.TypeKnowledgeRegistry.CurrentType;
            // TODO: Select amoung possibilities.

            

            return type;
        }
    }
}