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
            var visitors = treeRoots.Select(tree => new CompilationUnitVisitor(tree));
            return visitors.GroupBy(v => v.GetTopNamespace()).ToDictionary(g => g.Key, g => new Action<IndentedTextWriter>((textWriter) =>
            {
                var ordered = g.OrderBy(v => string.Join(".", v.GetNamespaceName()) + "." + v.GetElementName() + "." + v.GetNumGenericsOfElement()).ToArray();
                var last = ordered.Last();
                var previousNamespace = new string[] { };

                for (var index = 0; index < ordered.Length; index++)
                {
                    var visitor = ordered[index];

                    var nameSpace = visitor.GetNamespaceName();
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

                    this.Visit(visitor, index > 0 ? ordered[index - 1] : null, index + 1 < ordered.Length ? ordered[index + 1] : null, textWriter);

                    previousNamespace = nameSpace;

                    if (visitor != last) continue;

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
        }

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