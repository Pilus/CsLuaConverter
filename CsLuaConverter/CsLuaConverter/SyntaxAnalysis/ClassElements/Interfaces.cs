

namespace CsLuaConverter.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Providers;
    using Providers.TypeProvider;

    class Interfaces : ILuaElement
    {
        private List<BaseList> baseLists;
        private GenericsDefinition generics;

        public Interfaces(List<BaseList> baseLists, GenericsDefinition generics)
        {
            this.baseLists = baseLists;
            this.generics = generics;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            
            throw new NotImplementedException();
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            foreach (var baseList in this.baseLists)
            {
                var list = baseList;
                WriteElement(textWriter, () =>
                {
                    if (list.Name.Generics == null)
                    {
                        textWriter.Write("{0}()", list.GetFullName(providers));
                    }
                    else
                    {
                        textWriter.Write("{0}(", list.GetFullName(providers));
                        list.Name.Generics.WriteLua(textWriter, providers);
                        textWriter.Write(")");
                    }
                    
                });
            }
        }

        private static void WriteElement(IndentedTextWriter textWriter, Action value)
        {
            LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>
            {
                {"type", ElementType.Interface},
                {"value", value },
            },",", null);
        }
    }
}
