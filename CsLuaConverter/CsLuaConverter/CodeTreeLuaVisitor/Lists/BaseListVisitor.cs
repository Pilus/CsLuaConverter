namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.IO;
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumElements()
        {
            return this.nameVisitors.Length;
        }

        public bool WriteInteractiveObjectRefOfFirstTypeIfClass(IIndentedTextWriterWrapper textWriter, IProviders providers)
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

            first.WriteAsReference(textWriter, providers);

            return true;
        }

        public void WriteInterfaceImplements(IIndentedTextWriterWrapper textWriter, IProviders providers, string format, Type[] excludedTypes = null)
        {
            foreach (var visitor in this.nameVisitors)
            {
                var type = providers.TypeProvider.LookupType(visitor.GetName());

                if (!type.IsInterface || (excludedTypes != null && excludedTypes.Contains(type.TypeObject))) continue;

                var writer = new StringWriter();
                var innerWriter = new IndentedTextWriterWrapper(writer);
                innerWriter.Indent = textWriter.Indent;
                visitor.WriteAsType(innerWriter, providers);
                textWriter.WriteLine(format, writer);
            }
        }
    }
}