namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class PredefinedTypeVisitor : IVisitor<PredefinedType>
    {
        public void Visit(PredefinedType element, IndentedTextWriter textWriter, IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(element.Text);
            textWriter.Write("{0}.__typeof", type.GetTypeObject().FullName);
        }
    }
}