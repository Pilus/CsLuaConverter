﻿namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class BaseListVisitor : BaseVisitor, IListVisitor
    {
        private readonly INameVisitor[] nameVisitors;
        public BaseListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ColonToken);
            this.nameVisitors =
                this.CreateVisitors(new KindFilter(SyntaxKind.IdentifierName, SyntaxKind.QualifiedName,
                    SyntaxKind.GenericName)).Select(v => (INameVisitor) v).ToArray();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumElements()
        {
            return this.nameVisitors.Length;
        }

        public bool WriteInteractiveObjectRefOfFirstTypeIfClass(IndentedTextWriter textWriter, IProviders providers)
        {
            var first = this.nameVisitors.FirstOrDefault();
            if (first == null)
            {
                return false;
            }

            var type = providers.TypeProvider.LookupType(first.GetName());
            if (!type.IsClass)
            {
                return false;
            }

            first.WriteAsType(textWriter, providers);

            return true;
        }
    }
}