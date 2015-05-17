namespace CsToLua.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal class Properties : ILuaElement
    {
        private readonly bool isStatic;
        private readonly List<Property> properties;

        public Properties(bool isStatic, List<Property> properties)
        {
            this.isStatic = isStatic;
            this.properties = properties;
        }

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            foreach (Property property in this.properties)
            {
                property.WriteLua(textWriter, nameProvider);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }
    }
}