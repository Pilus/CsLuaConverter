namespace CsToLua.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;

    internal class Variables : ILuaElement
    {
        private readonly bool isStatic;
        private readonly List<ClassVariable> variables;

        public Variables(bool isStatic, List<ClassVariable> variables)
        {
            this.isStatic = isStatic;
            this.variables = variables;
        }

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            this.variables.ForEach(variable => variable.WriteLua(textWriter, nameProvider));
        }


        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }
    }
}