namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class PredefinedTypeVisitor : BaseOpenCloseVisitor<PredefinedType>
    {
        protected override void Write(PredefinedType element, IndentedTextWriter textWriter, IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(element.Text);
            textWriter.Write("{0}", type.GetTypeObject().FullName);
        }
    }
}