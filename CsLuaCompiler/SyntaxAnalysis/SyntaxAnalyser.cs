namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class SyntaxAnalyser
    {
        private SyntaxToken token;

        public NameSpacePart AnalyseDocument(Document document)
        {
            SyntaxNode syntaxTreeRoot = this.GetSyntaxTreeRoot(document);
            this.token = syntaxTreeRoot.FindToken(syntaxTreeRoot.SpanStart);
            try
            {
                this.PrintSyntaxTreeToFile(document.Name, this.token);
            }
            catch (Exception)
            {
            }

            if (Debugger.IsAttached)
            {
                return this.AnalyseDocumentContent();
            }

            try
            {
                return this.AnalyseDocumentContent();
            }
            catch (Exception ex)
            {
                throw new WrappingException(string.Format("In file: {0}.", document.Name), ex);
            }
        }

        private SyntaxNode GetSyntaxTreeRoot(Document doc)
        {
            Task<SyntaxTree> task = doc.GetSyntaxTreeAsync();
            task.Wait();
            SyntaxTree tree = task.Result;
            Task<SyntaxNode> rootTask = tree.GetRootAsync();
            rootTask.Wait();
            return rootTask.Result;
        }

        private void PrintSyntaxTreeToFile(string name, SyntaxToken token)
        {
            string s = "Token Parent,Text\n";
            SyntaxToken firstToken = token;
            while (token.Parent != null)
            {
                s += string.Format("{0},{1}\n", token.Parent.GetType().Name, token.Text);
                token = token.GetNextToken();
            }
            var i = 2;
            var fileName = "DebugOutput\\" + name + ".csv";
            while (File.Exists(fileName))
            {
                fileName = "DebugOutput\\" + name + "." + i + ".csv";
                i++;
            }
            File.WriteAllText(fileName, s);
        }

        private NameSpacePart AnalyseDocumentContent()
        {
            while (this.token != null && this.token.Parent != null)
            {
                switch (this.token.Parent.GetType().Name)
                {
                    case "UsingDirectiveSyntax":
                        this.token = this.IgnoreUsingDirective(this.token);
                        break;
                    case "NamespaceDeclarationSyntax":
                        var ns = new NameSpacePart();
                        ns.Analyze(this.token);
                        return ns;
                    case "CompilationUnitSyntax":
                        break;
                    default:
                        throw new Exception(string.Format("Unexpeted token in document: {0}.",
                            this.token.Parent.GetType().Name));
                }
                this.token = this.token.GetNextToken();
            }
            throw new Exception("No namespace found in file.");
        }

        private SyntaxToken IgnoreUsingDirective(SyntaxToken token)
        {
            this.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // using
            token = token.GetNextToken();
            this.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x

            while (token.GetNextToken().Parent.GetType().Equals(typeof(QualifiedNameSyntax)))
            {
                token = token.GetNextToken();
                this.CheckType(typeof(QualifiedNameSyntax), token.Parent); // .
                token = token.GetNextToken();
                this.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x                              
            }

            token = token.GetNextToken();
            this.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // ;

            return token;
        }


        /*
        private SyntaxToken WaitFor(Type type, SyntaxToken token)
        {
            while (token != null && token.Parent != null)
            {
                var parent = token.Parent;
                if (parent.GetType().Equals(type))
                {
                    return token;
                }
                token = token.GetNextToken();
            }
            return new SyntaxToken();
        }*/

        private void CheckType(Type expectedType, object obj)
        {
            this.CheckType(new[] {expectedType}, obj);
        }

        private void CheckType(IEnumerable<Type> expectedTypes, object obj)
        {
            string expected = string.Format("Expected token type {0}.",
                string.Join(", ", expectedTypes.Select(et => et.Name)));
            if (obj == null) throw new Exception("The object is null. " + expected);

            if (!expectedTypes.Contains(obj.GetType()))
                throw new Exception(string.Format("The object was not of expected type. {0} Got {1}.", expected,
                    obj.GetType().Name));
        }
    }
}