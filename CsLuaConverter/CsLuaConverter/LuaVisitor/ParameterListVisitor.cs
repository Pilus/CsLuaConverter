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
            
            foreach (var subElement in element.ContainedElements)
            {
                if (!first)
                {
                    textWriter.Write(",");
                }

                VisitorList.Visit(subElement);
                first = false;
            }
        }
    }
}