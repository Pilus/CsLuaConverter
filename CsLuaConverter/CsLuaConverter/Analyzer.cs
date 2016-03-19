namespace CsLuaConverter
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeElementAnalysis;
    using CodeTree;
    using CodeTreeLuaVisitor;
    using LuaVisitor;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using TypeKnowledgeVisitor;
    using ProjectInfo = ProjectAnalysis.ProjectInfo;

    public class Analyzer : ISyntaxAnalyser
    {
        private readonly IDocumentVisitor documentVisitor;
        private readonly CodeTreeVisitor codeTreeVisitor;

        public Analyzer(IProviders providers)
        {
            this.documentVisitor = new LuaDocumentVisitor(providers);
            this.codeTreeVisitor = new CodeTreeVisitor(providers);
        }

        public AnalyzedProjectInfo AnalyzeProject(ProjectInfo projectInfo)
        {
            return new AnalyzedProjectInfo()
            {
                Info = projectInfo,
                Namespaces = projectInfo.IsCsLua() ? this.GetNamespaces(projectInfo.Project) : null,
            };
        }

        private Dictionary<string, Action<IndentedTextWriter>> GetNamespaces(Project project)
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

        private Dictionary<string, Action<IndentedTextWriter>> GetNamespacesFromProject(Project project)
        {
            IEnumerable<Document> docs = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            //var documentElements = docs.Select(AnalyzeDocument).ToArray();

            //return this.documentVisitor.Visit(documentElements);

            //var debugInfo = WriteAllKinds(docs);

            var codeTrees = docs.Select(GetCodeTree).ToArray();

            return this.codeTreeVisitor.CreateNamespaceBasedVisitorActions(codeTrees);
        }

        private static string WriteAllKinds(IEnumerable<Document> docs)
        {
            return string.Join("\n", docs.Select(doc =>
            {
                SyntaxNode syntaxTreeRoot = GetSyntaxTreeRoot(doc);
                return ShowAllTokens(syntaxTreeRoot.GetFirstToken());
            }));
        }


        private static CodeTreeBranch GetCodeTree(Document document)
        {
            SyntaxNode syntaxTreeRoot = GetSyntaxTreeRoot(document);
            return new CodeTreeBranch(syntaxTreeRoot, document.FilePath);
        }

        private static SyntaxToken firstToken;

        private static DocumentElement AnalyzeDocument(Document document)
        {
            
            SyntaxNode syntaxTreeRoot = GetSyntaxTreeRoot(document);
            var token = syntaxTreeRoot.FindToken(syntaxTreeRoot.SpanStart);
            firstToken = token;
            var element = new DocumentElement()
            {
                Name = document.Name
            };

            if (Debugger.IsAttached)
            {
                element.Analyze(token);
            }
            else
            {
                try
                {
                    element.Analyze(token);
                }
                catch (Exception ex)
                {

                    throw new WrappingException(string.Format("In analysis of document: {0}.", document.Name), ex);
                }
            }

            return element; 
        }

        public static string DisplayKinds(SyntaxToken focusToken)
        {
            var token = firstToken;
            var list = new List<string[]>();
            while (!token.IsKind(SyntaxKind.None))
            {
                list.Add(new string[] { token.Text, SyntaxExtension.GetKind(token).ToString(), SyntaxExtension.GetKind(token.Parent).ToString() });

                if (token == focusToken)
                {
                    list.Insert(0, new string[] { "Token at index:", (list.Count + 1).ToString() });
                }

                token = token.GetNextToken();
            }

            var max1 = list.Max(i => i[0].Length);
            var max2 = list.Max(i => i[1].Length);

            var sw = new StringWriter();

            foreach (var line in list)
            {
                if (line.Length > 0)
                {
                    sw.Write(line[0] + new string(' ', 1 + max1 - line[0].Length));
                }

                if (line.Length > 1)
                {
                    sw.Write(line[1] + new string(' ', 1 + max2 - line[1].Length));
                }

                if (line.Length > 2)
                {
                    sw.Write(line[2]);
                }

                sw.WriteLine("");
            }

            File.WriteAllText("debug.txt", sw.ToString());

            return new FileInfo("debug.txt").FullName;
        }

        private static string ShowAllTokens(SyntaxToken token)
        {
            var sw = new StringWriter();

            while (token != null && token.Parent != null)
            {
                var line = SyntaxExtension.GetKind(token).ToString();
                var parent = token.Parent;

                while (parent != null)
                {
                    line = SyntaxExtension.GetKind(parent) + "\t" + line;
                    parent = parent.Parent;
                }

                line = token.Text + "\t" + line;
                sw.WriteLine(line);
                token = token.GetNextToken();
            }

            return sw.ToString();
        }

        private static SyntaxNode GetSyntaxTreeRoot(Document doc)
        {
            Task<SyntaxTree> task = doc.GetSyntaxTreeAsync();
            task.Wait();
            SyntaxTree tree = task.Result;
            Task<SyntaxNode> rootTask = tree.GetRootAsync();
            rootTask.Wait();
            return rootTask.Result;
        }
    }
}