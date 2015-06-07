

namespace CsLuaCompiler.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Providers;

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
            throw new NotImplementedException();
        }
    }
}
