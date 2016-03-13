namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Reflection;
    using CodeTree;
    using Providers;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseVisitor : IVisitor
    {
        protected readonly CodeTreeBranch Branch;

        protected BaseVisitor(CodeTreeBranch branch)
        {
            this.Branch = branch;
        }

        public abstract void Visit(IndentedTextWriter textWriter, IProviders providers);

        public T Visit<T>(int i) where T: BaseVisitor
        {
            var subBranch = this.Branch.Nodes[i] as CodeTreeBranch;
            if (subBranch == null)
            {
                throw new VisitorException($"Non branch found on node {i}.");
            }

            var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name.Equals(subBranch.Kind.ToString() + "Visitor") && t.IsSubclassOf(typeof(BaseVisitor)));

            if (type == null)
            {
                throw new VisitorException($"No visitor found for {subBranch.Kind}.");
            }

            return type.GetConstructors().Single().Invoke(new object[] { subBranch }) as T;
        }

        protected static bool IsKind(CodeTreeNode node, SyntaxKind kind)
        {
            return node.Kind == kind;
        }
    }
}