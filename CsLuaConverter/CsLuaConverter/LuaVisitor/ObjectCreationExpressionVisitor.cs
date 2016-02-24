namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ObjectCreationExpressionVisitor : IVisitor<ObjectCreationExpression>
    {
        public void Visit(ObjectCreationExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            var e = element.TypeElement;

            if (e is IdentifierName)
            {
                IdentifierNameVisitor.Visit(e as IdentifierName, textWriter, providers, IdentifyerType.AsRef);
            }
            else if (e is GenericName)
            {
                GenericNameVisitor.Visit(e as GenericName, textWriter, providers, false);
            }
            else
            {
                throw new LuaVisitorException("Unexected element in object creation expression.");
            }
        }
    }
}