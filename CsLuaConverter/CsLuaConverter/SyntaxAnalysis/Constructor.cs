namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Constructor : IFunction
    {
        private ArgumentList baseCall;
        private string baseCallPrefix;
        private Block block;
        private ParameterList parameters;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("function(element");
            if (this.parameters.Parameters.Count > 0)
            {
                textWriter.WriteLine(", ");
                this.parameters.WriteLua(textWriter, providers);
            }
            
            textWriter.WriteLine(")");
            textWriter.Indent++;

            if (this.baseCall != null)
            {
                // TODO: Modify to correct base constructor call.
                textWriter.Write(this.baseCallPrefix);
                this.baseCall.WriteLua(textWriter, providers);
                textWriter.WriteLine(";");
            }
            else
            {
                textWriter.WriteLine("_M.AM(baseConstructors,{}).func(element);");
            }

            this.block.WriteLua(textWriter, providers);
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
                return "element.__base";
            }

            if (type.Equals("this"))
            {
                return "element";
            }
            throw new Exception("Unknown base type");
        }
    }
}