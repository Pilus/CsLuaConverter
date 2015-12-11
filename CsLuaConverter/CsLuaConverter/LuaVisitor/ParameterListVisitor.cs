namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;

    public class ParameterListVisitor : IVisitor<ParameterList>
    {
        public void Visit(ParameterList element, IndentedTextWriter textWriter, IProviders providers)
        {
            var first = true;
            
            foreach (var parameterElements in element.ContainedElements)
            {
                if (!first)
                {
                    textWriter.Write(",");
                }

                foreach (var parameterElement in parameterElements)
                {
                    VisitorList.Visit(parameterElement);
                }
                

                first = false;
            }
        }
    }
}