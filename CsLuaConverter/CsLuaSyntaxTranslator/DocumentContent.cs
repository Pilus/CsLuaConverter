namespace CsLuaSyntaxTranslator
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class DocumentContent
    {
        public DocumentContent(Document document)
        {
            this.Syntax = (CompilationUnitSyntax)document.GetSyntaxRootAsync().Result;
            this.SemanticModel = document.GetSemanticModelAsync().Result;
            this.Path = document.FilePath;
        }

        public DocumentContent(CompilationUnitSyntax syntax, string path, SemanticModel semanticModel)
        {
            this.Syntax = syntax;
            this.Path = path;
            this.SemanticModel = semanticModel;
        }

        public CompilationUnitSyntax Syntax { get; private set; }

        public string Path { get; private set; }

        public SemanticModel SemanticModel { get; private set; }
        
    }
}