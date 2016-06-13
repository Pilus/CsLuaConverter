namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CodeTreeVisitor
    {
        private readonly IProviders providers;

        public CodeTreeVisitor(IProviders providers)
        {
            this.providers = providers;
        }

        public Dictionary<string, Action<IIndentedTextWriterWrapper>> CreateNamespaceBasedVisitorActions(CodeTreeBranch[] treeRoots)
        {
            BaseVisitor.LockVisitorCreation = false;
            treeRoots = treeRoots.SelectMany(SeperateCodeElements).ToArray();
            var visitors = treeRoots.Select(tree => new CompilationUnitVisitor(tree)).ToArray();
            BaseVisitor.LockVisitorCreation = true;

            return visitors.GroupBy(v => v.GetTopNamespace()).ToDictionary(g => g.Key, g => new Action<IIndentedTextWriterWrapper>((textWriter) =>
            {
                var fileGroups = g.GroupBy(v => string.Join(".", v.GetNamespaceName()) + ".__" + v.GetElementName());
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

                // Write footer
                foreach (var compilationUnitVisitor in g)
                {
                    compilationUnitVisitor.WriteFooter(textWriter, this.providers);
                }
            }));
        }

        private static CodeTreeBranch[] SeperateCodeElements(CodeTreeBranch tree)
        {
            var numElements = CountNumOfElements(tree);

            if (numElements < 2)
            {
                return new []{ tree };
            }

            var newTrees = new List<CodeTreeBranch>();

            for (var i = 0; i < numElements; i++)
            {
                var clone = (CodeTreeBranch)tree.Clone();
                FilterElements(clone, i);
                newTrees.Add(clone);
            }

            return newTrees.ToArray();
        }

        private static bool IsCodeElement(CodeTreeNode t)
        {
            return t.Kind == SyntaxKind.ClassDeclaration || t.Kind == SyntaxKind.InterfaceDeclaration ||
            t.Kind == SyntaxKind.EnumDeclaration || t.Kind == SyntaxKind.StructDeclaration;
        }

        private static void FilterElements(CodeTreeBranch branch, int indexToKeep)
        {
            switch (branch.Kind)
            {
                case SyntaxKind.CompilationUnit:
                    branch.Nodes.OfType<CodeTreeBranch>()
                            .Where(t => t.Kind == SyntaxKind.NamespaceDeclaration)
                            .ToList().ForEach(ns => FilterElements(ns, indexToKeep));
                    break;
                case SyntaxKind.NamespaceDeclaration:
                    var elementToKeep = branch.Nodes.OfType<CodeTreeBranch>().Where(IsCodeElement).Skip(indexToKeep).First();
                    branch.Nodes =
                        branch.Nodes.Where(n => n is CodeTreeLeaf || !IsCodeElement(n) || n == elementToKeep).ToArray();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static int CountNumOfElements(CodeTreeBranch tree)
        {
            switch (tree.Kind)
            {
                case SyntaxKind.CompilationUnit:
                    return
                        tree.Nodes.OfType<CodeTreeBranch>()
                            .Where(t => t.Kind == SyntaxKind.NamespaceDeclaration)
                            .Sum(CountNumOfElements);
                case SyntaxKind.NamespaceDeclaration:
                    return tree.Nodes.OfType<CodeTreeBranch>().Count(IsCodeElement);
                default:
                    throw new NotImplementedException();
            }
        }

        private void VisitFilesWithSameElementName(CompilationUnitVisitor[] visitors, IIndentedTextWriterWrapper textWriter)
        {
            var name = visitors.First().GetElementName();
            textWriter.WriteLine($"{name} = _M.NE({{");
            textWriter.Indent++;

            var visitorsByNumGenerics = new Dictionary<int, List<CompilationUnitVisitor>>();

            foreach (var visitor in visitors)
            {
                var numberOfGenericsSet = visitor.GetNumGenericsOfElement();
                foreach (var i in numberOfGenericsSet)
                {
                    if (!visitorsByNumGenerics.ContainsKey(i))
                    {
                        visitorsByNumGenerics[i] = new List<CompilationUnitVisitor>();
                    }

                    visitorsByNumGenerics[i].Add(visitor);
                }
            }

            foreach (var visitorsWithSameNumGenerics in visitorsByNumGenerics.OrderBy(v => v.Key))
            {
                this.VisitFilesWithSameElementNameAndNumGenerics(visitorsWithSameNumGenerics.Value.ToArray(), textWriter, visitorsWithSameNumGenerics.Key);
            }

            textWriter.Indent--;
            textWriter.WriteLine("}),");
        }

        private void VisitFilesWithSameElementNameAndNumGenerics(CompilationUnitVisitor[] visitors, IIndentedTextWriterWrapper textWriter, int numOfGenerics)
        {
            var state = this.providers.PartialElementState;
            state.CurrentState = null;
            state.NumberOfGenerics = numOfGenerics;
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
        private void Visit(CompilationUnitVisitor visitor, CompilationUnitVisitor previousVisitor, CompilationUnitVisitor nextVisitor, IIndentedTextWriterWrapper textWriter)
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