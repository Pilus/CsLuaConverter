namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class TypeArgumentListVisitor : BaseVisitor, IListVisitor
    {
        public TypeArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumElements()
        {
            throw new System.NotImplementedException();
        }
    }
}