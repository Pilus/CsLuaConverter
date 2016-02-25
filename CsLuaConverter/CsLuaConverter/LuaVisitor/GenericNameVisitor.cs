namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class GenericNameVisitor : IOpenCloseVisitor<GenericName>
    {
        public void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            Visit(element, textWriter, providers, false);
        }

        public static void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers,
            bool writeAsIs)
        {
            WriteOpen(element, textWriter, providers, writeAsIs, new string[] {});
            WriteClose(element, textWriter, providers, writeAsIs, new string[] { });
        }

        public static void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers,
            bool writeAsIs, IEnumerable<string> precidingNames)
        {
            WriteOpen(element, textWriter, providers, writeAsIs, precidingNames);
            WriteClose(element, textWriter, providers, writeAsIs, precidingNames);
        }

        public void WriteOpen(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteOpen(element, textWriter, providers, true, new string[] { });
        }

        public void WriteClose(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteClose(element, textWriter, providers, true, new string[] { });
        }

        private static void WriteOpen(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup, IEnumerable<string> precidingNames)
        {
            VisitorList.WriteOpen(element.InnerElement);

            if (element.IsArray)
            {
                textWriter.Write("System.Array[{");
            }

            var names = GetNames(element, textWriter, providers, skipLookup, precidingNames);

            textWriter.Write(new string('(', names.Count + (element.InnerElement != null ? 1 : 0)));
        }

        private static int GetNumGenerics(GenericName element)
        {
            return element.ArgumentList.ContainedElements.Count;
        }

        private static List<string> GetNames(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup, IEnumerable<string> precidingNames)
        {
            var allNameElements = precidingNames.ToList();
            allNameElements.Add(element.Name);
            var numGenerics = GetNumGenerics(element);
            return skipLookup ? new List<string>() { element.Name } : providers.NameProvider.LookupVariableNameSplitted(allNameElements, numGenerics).ToList();
        }

        private static void WriteClose(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup, IEnumerable<string> precidingNames)
        {
            var names = GetNames(element, textWriter, providers, skipLookup, precidingNames);

            for (var i = 0; i<names.Count; i++)
            {
                var name = names[i];
                if (i != 0)
                {
                    textWriter.Write(".");
                }

                textWriter.Write(name);

                if (i < names.Count)
                { 
                    if (name == "element")
                    {
                        textWriter.Write(" % _M.DOT_LVL(typeObject.Level))");
                    }
                    else
                    {
                        textWriter.Write(" % _M.DOT)");
                    }
                }
            }

            VisitorList.Visit(element.ArgumentList);

            if (element.IsArray)
            {
                textWriter.Write(".__typeof}]");
            }

            if (element.InnerElement != null)
            {
                textWriter.Write(" % _M.DOT)");
                VisitorList.WriteClose(element.InnerElement);
            }
        }
    }
}