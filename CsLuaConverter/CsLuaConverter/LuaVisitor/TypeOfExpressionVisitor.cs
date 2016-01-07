namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class TypeOfExpressionVisitor : IVisitor<TypeOfExpression>
    {
        public void Visit(TypeOfExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.Visit(element.ContainedElements.Single());
            textWriter.Write(".__typeof");
        }
    }
}