namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CodeTreeVisitor
    {
        private readonly IContext context;

        public CodeTreeVisitor(IContext context)
        {
            this.context = context;
        }

        public IEnumerable<Namespace> CreateNamespaceBasedVisitorActions(Tuple<CodeTreeBranch, SemanticModel>[] treeRoots)
        {
            BaseVisitor.LockVisitorCreation = false;
            //treeRoots = treeRoots.SelectMany(SeperateCodeElements).ToArray();
            var visitors = treeRoots.Select(tree => new CompilationUnitVisitor(tree.Item1, tree.Item2)).ToArray();
            //BaseVisitor.LockVisitorCreation = true;

            return visitors.GroupBy(v => v.GetTopNamespace()).Select(g => new Namespace() {
                Name = g.Key,
                WritingAction = new Action<IIndentedTextWriterWrapper>((textWriter) =>
            {
                var fileGroups = g.GroupBy(v => string.Join(".", v.GetNamespaceName()) + ".__" + v.GetElementName());
                var ordered = fileGroups.OrderBy(fg => fg.Key).ToArray();
                
                for (var index = 0; index < ordered.Length; index++)
                {
                    var groupedVisitors = ordered[index].ToArray();

                    this.VisitFilesWithSameElementName(groupedVisitors, textWriter);
                }

                // Write footer
                foreach (var compilationUnitVisitor in g)
                {
                    compilationUnitVisitor.WriteFooter(textWriter, this.context);
                }
            })});
        }
        

        private void VisitFilesWithSameElementName(CompilationUnitVisitor[] visitors, IIndentedTextWriterWrapper textWriter)
        {
            var first = visitors.First();
            var name = first.GetElementName();
            var fullNamespace = string.Join(".", first.GetNamespaceName());
            textWriter.WriteLine($"_M.ATN('{fullNamespace}','{name}', _M.NE({{");
            textWriter.Indent++;

            var visitorsByNumGenerics = new Dictionary<int, List<DocumentContent>>();

            foreach (var visitor in visitors)
            {
                var numberOfGenericsSet = visitor.GetNumGenericsOfElement().Distinct();
                foreach (var i in numberOfGenericsSet)
                {
                    if (!visitorsByNumGenerics.ContainsKey(i))
                    {
                        visitorsByNumGenerics[i] = new List<DocumentContent>();
                    }

                    visitorsByNumGenerics[i].Add(visitor.AsDocumentContent());
                }
            }

            foreach (var visitorsWithSameNumGenerics in visitorsByNumGenerics.OrderBy(v => v.Key))
            {
                this.WriteContentsWithSameElementNameAndNumGenerics(visitorsWithSameNumGenerics.Value.ToArray(), textWriter, visitorsWithSameNumGenerics.Key);
            }

            textWriter.Indent--;
            textWriter.WriteLine("}));");
        }

        private void WriteContentsWithSameElementNameAndNumGenerics(DocumentContent[] contents, IIndentedTextWriterWrapper textWriter, int numOfGenerics)
        {
            var state = this.context.PartialElementState;
            state.CurrentState = null;
            state.DefinedConstructorWritten = false;

            state.NumberOfGenerics = numOfGenerics;
            while (true)
            {
                for (int index = 0; index < contents.Length; index++)
                {
                    var content = contents[index];
                    state.IsFirst = index == 0;
                    state.IsLast = index == contents.Length - 1;

                    this.context.SemanticModel = content.SemanticModel;
                    //this.context.DocumentPath = content.Path;
                    content.Syntax.Write(textWriter, this.context);
                }

                if (state.NextState == null)
                {
                    break;
                }

                state.CurrentState = state.NextState;
            }
        }
    }
}