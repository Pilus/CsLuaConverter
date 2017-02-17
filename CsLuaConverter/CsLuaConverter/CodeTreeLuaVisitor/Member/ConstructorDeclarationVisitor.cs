namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Lists;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ConstructorDeclarationVisitor : BaseVisitor
    {
        private readonly Scope scope;
        private readonly ParameterListVisitor parameterList;
        private readonly BlockVisitor block;
        private readonly BaseConstructorInitializerVisitor baseConstructorInitializer;
        private readonly ThisConstructorInitializerVisitor thisConstructorInitializer;

        public ConstructorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.PublicKeyword, SyntaxKind.ProtectedKeyword);
            this.scope = (Scope)Enum.Parse(typeof(Scope), ((CodeTreeLeaf)this.Branch.Nodes[0]).Text, true);
            this.parameterList =
                (ParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.ParameterList)).Single();
            this.block =
                (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).Single();
            this.baseConstructorInitializer =
                (BaseConstructorInitializerVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.BaseConstructorInitializer)).SingleOrDefault();
            this.thisConstructorInitializer =
                (ThisConstructorInitializerVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ThisConstructorInitializer)).SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            ((ConstructorDeclarationSyntax) this.Branch.SyntaxNode).Visit(textWriter, context);
        }
    }
}