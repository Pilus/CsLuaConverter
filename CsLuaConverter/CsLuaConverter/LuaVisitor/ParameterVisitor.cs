namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public class ParameterVisitor : IVisitor<Parameter>
    {
        public void Visit(Parameter element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.IsParams)
            {
                return;
            }

            providers.NameProvider.AddToScope(new ScopeElement(element.Name));
            textWriter.Write(element.Name);
        }
    }
}