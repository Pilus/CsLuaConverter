namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class BlockVisitor : IVisitor<Block>
    {
        public void Visit(Block element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("-- TODO: BlockVisitor");
            //throw new System.NotImplementedException();
        }
    }
}