namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class TypeOfExpressionVisitor : BaseOpenCloseVisitor<TypeOfExpression>
    {
        protected override void Write(TypeOfExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteTypeReference(element.Type, textWriter, providers);
        }

        public static void WriteTypeReference(BaseElement typeElement, IndentedTextWriter textWriter,
            IProviders providers)
        {
            var skipTypeOf = false;
            if (typeElement is IdentifierName)
            {
                var e = typeElement as IdentifierName;
                var isGeneric = providers.GenericsRegistry.IsGeneric(e.Names.First());
                IdentifierNameVisitor.Visit(e, textWriter, providers, isGeneric ? IdentifyerType.AsGeneric : IdentifyerType.AsRef);
                skipTypeOf = isGeneric;
            }
            else
            {
                VisitorList.Visit(typeElement);
            }

            if (skipTypeOf)
            {
                return;
            }

            textWriter.Write(".__typeof");
        }
    }
}