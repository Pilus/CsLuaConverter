namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class IdentifierNameVisitor : IVisitor<IdentifierName>
    {
        public void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            Visit(element, textWriter, providers, false);
        }


        public static void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, bool skipVariableLookup)
        {
            if (element.Names.FirstOrDefault() != "var")
            {
                var skippedLevels = element.InnerElement == null ? 1 : 0;
                IList<string> names;
                if (skipVariableLookup)
                {
                    names = element.Names.ToList();
                }
                else
                {
                    names = providers.NameProvider.LookupVariableNameSplitted(element.Names).ToList();
                }

                textWriter.Write(new string('(', names.Count- skippedLevels));

                for (var i=0; i < names.Count; i++)
                {
                    var name = names[i];
                    if (i != 0)
                    {
                        textWriter.Write(".");
                    }

                    textWriter.Write(name);

                    if (i < names.Count - skippedLevels)
                    { 
                        if (name == "element")
                        {
                            textWriter.Write("%_M.DOT_LVL(typeObject.Level))");
                        }
                        else //if (i != names.Count - 1 && !(element.InnerElement is ArgumentList))
                        {
                            textWriter.Write("%_M.DOT)");
                        }
                    }
                }
                
            }
            else
            {
                textWriter.Write("local ");
            }

            if (element.InnerElement != null)
            {
                VisitorList.Visit(element.InnerElement);
            }
        }
    }
}