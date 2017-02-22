namespace CsLuaSyntaxTranslator
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class NamespaceMember
    {
        public NamespaceMember(DocumentContent document, BaseTypeDeclarationSyntax syntax)
        {
            this.Syntax = syntax;
            this.SemanticModel = document.SemanticModel;
            this.Path = document.Path;
        }

        public BaseTypeDeclarationSyntax Syntax { get; private set; }

        public string Path { get; private set; }

        public SemanticModel SemanticModel { get; private set; }

        public string GetName()
        {
            return this.Syntax.Identifier.Text;
        }

        public string GetNamespaceName()
        {
            return ((NamespaceDeclarationSyntax) this.Syntax.Parent).Name.ToString();
        }

        public string GetFullName()
        {
            return $"{this.GetNamespaceName()}.{this.GetName()}";
        }

        public int GetNumGenerics()
        {
            var typeSyntax = this.Syntax as TypeDeclarationSyntax;
            return typeSyntax?.TypeParameterList?.Parameters.Count ?? 0;
        }
    }
}