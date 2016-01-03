namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class ArrayVisitor : IVisitor<ArrayCreationExpression>, IVisitor<ArrayInitializerExpression>, IVisitor<ImplicitArrayCreationExpression>
    {
        public void Visit(ArrayCreationExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("System.Array({");
            VisitorList.Visit(element.ElementType);
            textWriter.Write("})");

            VisitorList.Visit(element.Initializer);
        }

        public void Visit(ArrayInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".__Cstor({");

            if (element.ContainedElements.Count > 0)
            {
                textWriter.Write("[0]=");

                foreach (var containedElementList in element.ContainedElements)
                {
                    foreach (var baseElement in containedElementList)
                    {
                        VisitorList.Visit(baseElement);
                    }
                    
                    textWriter.Write(",");
                }
            }

            textWriter.Write("})");
        }

        public void Visit(ImplicitArrayCreationExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("System.Array({nil})");

            VisitorList.Visit(element.Initializer);
        }
    }
}