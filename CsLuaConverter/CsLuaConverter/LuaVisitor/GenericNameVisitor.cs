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
            WriteOpen(element, textWriter, providers, writeAsIs);
            WriteClose(element, textWriter, providers, writeAsIs);
        }

        public void WriteOpen(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteOpen(element, textWriter, providers, true);
        }

        public void WriteClose(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteClose(element, textWriter, providers, true);
        }

        private static void WriteOpen(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup)
        {
            VisitorList.WriteOpen(element.InnerElement);

            if (element.IsArray)
            {
                textWriter.Write("System.Array[{");
            }

            var names = GetNames(element, textWriter, providers, skipLookup);

            textWriter.Write(new string('(', names.Count + (element.InnerElement != null ? 1 : 0)));
        }

        private static int GetNumGenerics(GenericName element)
        {
            return element.ArgumentList.ContainedElements.Count;
        }

        private static List<string> GetNames(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup)
        {
            var numGenerics = GetNumGenerics(element);
            return skipLookup ? new List<string>() { element.Name } : providers.NameProvider.LookupVariableNameSplitted(new[] { element.Name }, numGenerics).ToList();
        }

        private static void WriteClose(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup)
        {
            var names = GetNames(element, textWriter, providers, skipLookup);

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