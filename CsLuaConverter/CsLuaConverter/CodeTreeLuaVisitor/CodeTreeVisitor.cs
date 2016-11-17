namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
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
            treeRoots = treeRoots.SelectMany(SeperateCodeElements).ToArray();
            var visitors = treeRoots.Select(tree => new CompilationUnitVisitor(tree.Item1, tree.Item2)).ToArray();
            BaseVisitor.LockVisitorCreation = true;

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

        private static Tuple<CodeTreeBranch, SemanticModel>[] SeperateCodeElements(Tuple<CodeTreeBranch, SemanticModel> pair)
        {
            var numElements = CountNumOfElements(pair.Item1);

            if (numElements < 2)
            {
                return new []{ pair };
            }

            var newTrees = new List<Tuple<CodeTreeBranch, SemanticModel>>();

            for (var i = 0; i < numElements; i++)
            {
                var clone = (CodeTreeBranch)pair.Item1.Clone();
                FilterElements(clone, i);
                newTrees.Add(new Tuple<CodeTreeBranch, SemanticModel>(clone, pair.Item2));
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
            var first = visitors.First();
            var name = first.GetElementName();
            var fullNamespace = string.Join(".", first.GetNamespaceName());
            textWriter.WriteLine($"_M.ATN('{fullNamespace}','{name}', _M.NE({{");
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
                //var scope = this.context.NameProvider.CloneScope();
                this.VisitFilesWithSameElementNameAndNumGenerics(visitorsWithSameNumGenerics.Value.ToArray(), textWriter, visitorsWithSameNumGenerics.Key);
                //this.context.NameProvider.SetScope(scope);
            }

            textWriter.Indent--;
            textWriter.WriteLine("}));");
            /*
            foreach (var visitor in visitors)
            {
                visitor.WriteExtensions(textWriter, this.context);
            } */
        }

        private void VisitFilesWithSameElementNameAndNumGenerics(CompilationUnitVisitor[] visitors, IIndentedTextWriterWrapper textWriter, int numOfGenerics)
        {
            var state = this.context.PartialElementState;
            state.CurrentState = null;
            state.DefinedConstructorWritten = false;

            state.NumberOfGenerics = numOfGenerics;
            while (true)
            {
                for (int index = 0; index < visitors.Length; index++)
                {
                    var visitor = visitors[index];
                    state.IsFirst = index == 0;
                    state.IsLast = index == visitors.Length - 1;
                    visitor.Visit(textWriter, this.context);
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

            visitor.Visit(textWriter, this.context);

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