namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ParameterVisitor : IVisitor<Parameter>
    {
        public void Visit(Parameter element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("RefTo_{0}", element.Name);
        }
    }
}