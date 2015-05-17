namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Constructor : IFunction
    {
        private ArgumentList baseCall;
        private string baseCallPrefix;
        private Block block;
        private ParameterList parameters;

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            textWriter.Write("function(");
            this.parameters.WriteLua(textWriter, nameProvider);
            textWriter.WriteLine(")");
            textWriter.Indent++;

            if (this.baseCall != null)
            {
                textWriter.Write(this.baseCallPrefix);
                this.baseCall.WriteLua(textWriter, nameProvider);
                textWriter.WriteLine(";");
            }
            else
            {
                textWriter.WriteLine("if class.__base then class.__base.__Cstor(); end");
            }

            this.block.WriteLua(textWriter, nameProvider);
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        public ParameterList GetParameters()
        {
            return this.parameters;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(ConstructorDeclarationSyntax), token.Parent); // public
            token = token.GetNextToken();

            LuaElementHelper.CheckType(typeof(ConstructorDeclarationSyntax), token.Parent); // Constructor name
            token = token.GetNextToken();

            this.parameters = new ParameterList();
            token = this.parameters.Analyze(token);
            token = token.GetNextToken();

            if (token.Parent is ConstructorInitializerSyntax) // :  For call to base constructor
            {
                token = token.GetNextToken(); // base
                this.baseCallPrefix = GetBaseCallPrefix(token.Text);
                token = token.GetNextToken();
                this.baseCall = new ArgumentList();
                token = this.baseCall.Analyze(token);
                token = token.GetNextToken();
            }

            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }

        private static string GetBaseCallPrefix(string type)
        {
            if (type.Equals("base"))
            {
                return "class.__base.__Cstor";
            }

            if (type.Equals("this"))
            {
                return "class.__Cstor";
            }
            throw new Exception("Unknown base type");
        }
    }
}