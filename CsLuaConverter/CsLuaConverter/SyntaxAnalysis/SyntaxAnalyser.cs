namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeElementAnalysis;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;

    internal class SyntaxAnalyser : ISyntaxAnalyser
    {
        private bool debugOutputFolderExists;
        private SyntaxToken token;
        private readonly List<string> usings = new List<string>();

        public SyntaxAnalyser()
        {
            var debugOutput = new DirectoryInfo("DebugOutput");
            this.debugOutputFolderExists = debugOutput.Exists;
            if (debugOutput.Exists)
            {
                foreach (FileInfo file in debugOutput.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        public AnalyzedProjectInfo AnalyzeProject(ProjectAnalysis.ProjectInfo projectInfo)
        {
            return new AnalyzedProjectInfo()
            {
                Info = projectInfo,
                Namespaces = projectInfo.IsCsLua() ? this.GetNamespaces(projectInfo.Project) : null,
            };
        }

        private Dictionary<string, Action<IndentedTextWriter, IProviders>> GetNamespaces(Project project)
        {
            if (Debugger.IsAttached)
            {
                return this.GetNamespacesFromProject(project);
            }

            try
            {
                return this.GetNamespacesFromProject(project);
            }
            catch (Exception ex)
            {

                throw new WrappingException(string.Format("In project: {0}.", project.Name), ex);
            }
        }


        private Dictionary<string, Action<IndentedTextWriter, IProviders>> GetNamespacesFromProject(Project project)
        {
            IEnumerable<Document> docs = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var nameSpaces = new Dictionary<string, NameSpace>();
            foreach (Document document in docs)
            {
                NameSpacePart nameSpacePart = this.AnalyseDocument(document);
                if (nameSpaces.ContainsKey(nameSpacePart.FullName.First()))
                {
                    nameSpaces[nameSpacePart.FullName.First()].AddPart(nameSpacePart);
                }
                else
                {
                    nameSpaces[nameSpacePart.FullName.First()] = new NameSpace(nameSpacePart, 1);
                }
            }

            return nameSpaces.ToDictionary<KeyValuePair<string, NameSpace>, string, Action<IndentedTextWriter, IProviders>>(ns => ns.Key, ns => ns.Value.WriteLua);
        }

        private NameSpacePart AnalyseDocument(Document document)
        {
            SyntaxNode syntaxTreeRoot = this.GetSyntaxTreeRoot(document);
            this.token = syntaxTreeRoot.FindToken(syntaxTreeRoot.SpanStart);
            
            if (this.debugOutputFolderExists)
            {
                this.PrintSyntaxTreeToFile(document.Name, this.token);
            }

            try
            {
                return this.AnalyseDocumentContent(document);
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
            
            while (token.Parent != null)
            {
                s += string.Format("{0},{1},{2}\n", token.Text, token.GetKind(), token.Parent.GetKind());
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

        private NameSpacePart AnalyseDocumentContent(Document document)
        {
            this.usings.Clear();
            while (this.token != null && this.token.Parent != null)
            {
                switch (this.token.Parent.GetType().Name)
                {
                    case "UsingDirectiveSyntax":
                        this.token = this.AnalyzeUsingDirective(this.token);
                        break;
                    case "NamespaceDeclarationSyntax":
                        var ns = new NameSpacePart(document.Name, this.usings);
                        this.token = ns.Analyze(this.token);
                        return ns;
                    case "CompilationUnitSyntax":
                        break;
                    case "AttributeListSyntax":
                        var attributes = new Attribute();
                        token = attributes.Analyze(token);
                        break;
                    default:
                        throw new Exception(string.Format("Unexpeted token in document: {0}.",
                            this.token.Parent.GetType().Name));
                }
                this.token = this.token.GetNextToken();
            }
            throw new Exception("No namespace found in file.");
        }

        private SyntaxToken AnalyzeUsingDirective(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // using
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x
            string name = token.Text;

            while (token.GetNextToken().Parent is QualifiedNameSyntax)
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(QualifiedNameSyntax), token.Parent); // .
                name += token.Text;

                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x  
                name += token.Text;
            }
            this.usings.Add(name);

            token = token.GetNextToken();
            if (token.Parent is NameEqualsSyntax) // = 
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x

                while (token.GetNextToken().Parent is QualifiedNameSyntax)
                {
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(typeof(QualifiedNameSyntax), token.Parent); // .
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
                    // x                              
                }
                token = token.GetNextToken();
            }
            LuaElementHelper.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // ;

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