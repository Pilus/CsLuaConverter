namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Providers;

    public class CodeTreeVisitor
    {
        private readonly IProviders providers;

        public CodeTreeVisitor(IProviders providers)
        {
            this.providers = providers;
        }

        public Dictionary<string, Action<IndentedTextWriter>> CreateNamespaceBasedVisitorActions(CodeTreeBranch[] treeRoots)
        {
            var visitors = treeRoots.Select(tree => new CompilationUnitVisitor(tree)).ToArray();
            BaseVisitor.LockVisitorCreation = true;

            return visitors.GroupBy(v => v.GetTopNamespace()).ToDictionary(g => g.Key, g => new Action<IndentedTextWriter>((textWriter) =>
            {
                var fileGroups = g.GroupBy(v => string.Join(".", v.GetNamespaceName()) + "." + v.GetElementName());
                var ordered = fileGroups.OrderBy(fg => fg.Key).ToArray();
                
                var previousNamespace = new string[] { };

                for (var index = 0; index < ordered.Length; index++)
                {
                    var groupedVisitors = ordered[index].ToArray();

                    var nameSpace = groupedVisitors.First().GetNamespaceName();
                    var nonCommonNamespace = RemoveCommonStartSequence(nameSpace, previousNamespace);

                    // Reset back to the common root with the previous namespace.
                    for (var i = 0;
                        i < (previousNamespace.Length - (nameSpace.Length - nonCommonNamespace.Length));
                        i++)
                    {
                        textWriter.Indent--;
                        textWriter.WriteLine("},");
                    }

                    foreach (var subNamespace in nonCommonNamespace)
                    {
                        textWriter.WriteLine($"{subNamespace} = {{");
                        textWriter.Indent++;
                    }

                    this.VisitFilesWithSameElementName(groupedVisitors, textWriter);

                    previousNamespace = nameSpace;

                    if (index != ordered.Length - 1) continue;

                    for (var i = 0; i < (nameSpace.Length - 1); i++)
                    {
                        textWriter.Indent--;
                        textWriter.WriteLine("},");
                    }

                    textWriter.Indent--;
                    textWriter.WriteLine("}");
                }
            }));
        }

        private void VisitFilesWithSameElementName(CompilationUnitVisitor[] visitors, IndentedTextWriter textWriter)
        {
            var name = visitors.First().GetElementName();
            textWriter.WriteLine($"{name} = _M.NE({{");
            textWriter.Indent++;

            foreach (var visitorsWithSameNumGenerics in visitors.GroupBy(v => v.GetNumGenericsOfElement()))
            {
                this.VisitFilesWithSameElementNameAndNumGenerics(visitorsWithSameNumGenerics.ToArray(), textWriter);
            }

            textWriter.Indent--;
            textWriter.WriteLine("}),");
        }

        private void VisitFilesWithSameElementNameAndNumGenerics(CompilationUnitVisitor[] visitors, IndentedTextWriter textWriter)
        {
            var state = this.providers.PartialElementState;
            state.CurrentState = null;
            while (true)
            {
                for (int index = 0; index < visitors.Length; index++)
                {
                    var visitor = visitors[index];
                    state.IsFirst = index == 0;
                    state.IsLast = index == visitors.Length - 1;
                    visitor.Visit(textWriter, this.providers);
                }

                if (state.NextState == null)
                {
                    break;
                }

                state.CurrentState = state.NextState;
            }
        }

        /*
        private void Visit(CompilationUnitVisitor visitor, CompilationUnitVisitor previousVisitor, CompilationUnitVisitor nextVisitor, IndentedTextWriter textWriter)
        {
            var previousName = previousVisitor?.GetElementName();

            var name = visitor.GetElementName();
            if (previousName != name)
            {
                textWriter.WriteLine($"{name} = _M.NE({{");
            }

            visitor.Visit(textWriter, this.providers);

            var nextName = nextVisitor?.GetElementName();
            if (nextName != name)
            {
                textWriter.WriteLine("}),");
            }
        } */

        private static string[] RemoveCommonStartSequence(string[] array, string[] comparedArray)
        {
            var i = 0;
            while (array.Length > i && comparedArray.Length > i && array[i] == comparedArray[i])
            {
                i++;
            }

            return array.Skip(i).ToArray();
        }
    }
}