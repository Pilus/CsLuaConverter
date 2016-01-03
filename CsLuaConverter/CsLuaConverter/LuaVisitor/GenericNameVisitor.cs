namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class GenericNameVisitor : IVisitor<GenericName>
    {
        public void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers)
        {
            Visit(element, textWriter, providers, false);
        }

        public static void Visit(GenericName element, IndentedTextWriter textWriter, IProviders providers, bool skipLookup)
        {
            var names = skipLookup ? new List<string>(){element.Name} : providers.NameProvider.LookupVariableNameSplitted(new [] { element.Name } ).ToList();

            textWriter.Write(new string('(', names.Count));

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
                        textWriter.Write("%_M.DOT_LVL(typeObject.Level))");
                    }
                    else
                    {
                        textWriter.Write("%_M.DOT)");
                    }
                }
            }

            VisitorList.Visit(element.ArgumentList);
        }
    }
}