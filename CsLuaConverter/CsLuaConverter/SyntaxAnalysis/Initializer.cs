namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Initializer : ILuaElement
    {
        private readonly List<ILuaElement[]> elements = new List<ILuaElement[]>();
        private readonly bool initializeClass;
        private readonly bool intializeClassWithoutContructor;

        public Initializer(bool initializeClass, bool intializeClassWithoutContructor)
        {
            this.initializeClass = initializeClass;
            this.intializeClassWithoutContructor = intializeClassWithoutContructor;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.initializeClass)
            {
                textWriter.Write(".__Initialize({");
            }
            else if (this.intializeClassWithoutContructor)
            {
                textWriter.Write("().__Initialize({");
            }
            else
            {
                textWriter.Write("System.Array(nil).__Cstor({");
            }

            for (int i = 0; i < this.elements.Count; i++)
            {
                LuaElementHelper.WriteLuaJoin(this.elements[i].ToList(), textWriter, providers, "");
                if (i < this.elements.Count - 1)
                {
                    textWriter.Write(",");
                }
            }
            textWriter.WriteLine("})");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(InitializerExpressionSyntax), token.Parent);

            bool first = true;
            while (!(token.Parent is InitializerExpressionSyntax && token.Text == "}"))
            {
                token = token.GetNextToken();

                ILuaElement firstElement = null;
                if (token.Parent is IdentifierNameSyntax && token.GetNextToken().Text == "=") // x = 
                {
                    firstElement = new PredeterminedElement(token.Text + " = ");
                    token = token.GetNextToken();
                    token = token.GetNextToken();
                }
                else if (first)
                {
                    // Let the first element be 0 indexed.
                    firstElement = new PredeterminedElement("[0] = ");
                }
                var code = new MainCode(t => t.Parent is InitializerExpressionSyntax && (t.Text == "," || t.Text == "}"));
                token = code.Analyze(token);

                this.elements.Add(firstElement != null ? new[] {firstElement, code} : new[] {code});
                first = false;
            }
            return token;
        }
    }
}