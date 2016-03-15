namespace CsLuaConverter.CodeTreeLuaVisitor.Filters
{
    using System.Collections.Generic;
    using CodeTree;

    public interface INodeFilter
    {
        IEnumerable<CodeTreeNode> Filter(IEnumerable<CodeTreeNode> nodes);
    }
}