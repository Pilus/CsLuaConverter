namespace CsLuaConverter.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;

    internal class Constructors : ILuaElement
    {
        private readonly List<Constructor> constructors;
        private readonly bool isSerializable;
        private readonly bool isStatic;
        private readonly string className;

        public Constructors(bool isStatic, bool isSerializable, List<Constructor> constructors, string className)
        {
            this.isStatic = isStatic;
            this.isSerializable = isSerializable;
            this.constructors = constructors;
            this.className = className;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.constructors.Any())
            {
                LuaFormatter.WriteClassElement(textWriter, ElementType.Constructor, "cstor", false, false, 
                    () => LuaFormatter.WriteMethodToLua(textWriter, providers, this.constructors), this.className);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }
    }
}