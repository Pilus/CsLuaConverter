namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Providers;

    public class Analyzer : ISyntaxAnalyser
    {
        private IDocumentVisitor documentVisitor;

        public Analyzer(IDocumentVisitor documentVisitor)
        {
            this.documentVisitor = documentVisitor;
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

            var documentElements = docs.Select(AnalyzeDocument);

            return this.documentVisitor.Visit(documentElements);
        }

        private static DocumentElement AnalyzeDocument(Document document)
        {
            SyntaxNode syntaxTreeRoot = GetSyntaxTreeRoot(document);
            var token = syntaxTreeRoot.FindToken(syntaxTreeRoot.SpanStart);
            var element = new DocumentElement();
            element.Analyze(token);
            return element;
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