namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class InterfaceDeclarationVisitor : IVisitor<InterfaceDeclaration>
    {
        public void Visit(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            //throw new System.NotImplementedException();
        }
    }
}