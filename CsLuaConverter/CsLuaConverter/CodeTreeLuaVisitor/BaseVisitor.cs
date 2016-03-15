namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using CodeTree;
    using Providers;
    using System.Linq;
    using Filters;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseVisitor : IVisitor
    {
        private readonly static Type[] baseVisitorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(BaseVisitor))).ToArray();

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

        protected BaseVisitor[] CreateVisitors(INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            return (filter == null ? this.Branch.Nodes : filter.Filter(this.Branch.Nodes)).OfType<CodeTreeBranch>()
                .Select(branch => customFactory?.Invoke(branch) ?? CreateVisitor(branch))
                .ToArray();
        }

        protected void CreateVisitorsAndVisitBranches(IndentedTextWriter textWriter, IProviders providers, INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            foreach (var visitor in this.CreateVisitors(filter, customFactory))
            {
                visitor.Visit(textWriter, providers);
            }
        }

        protected void ExpectKind(int index, SyntaxKind kind)
        {
            if (this.Branch.Nodes[index].Kind != kind)
            {
                throw new VisitorException($"Expected kind {kind} at index {index}. Got {this.Branch.Nodes[index].Kind}.");
            }
        }

        protected void ExpectKind(int index, SyntaxKind[] kinds)
        {
            if (!kinds.Contains(this.Branch.Nodes[index].Kind))
            {
                throw new VisitorException($"Expected kind {string.Join(", ", kinds)} at index {index}. Got {this.Branch.Nodes[index].Kind}.");
            }
        }

        protected BaseVisitor CreateVisitor(int index)
        {
            return CreateVisitor(this.Branch.Nodes[index] as CodeTreeBranch);
        }

        protected static BaseVisitor CreateVisitor(CodeTreeBranch branch)
        {
            var type = baseVisitorTypes.SingleOrDefault(t => t.Name.Equals(branch.Kind.ToString() + "Visitor"));

            if (type == null)
            {
                throw new VisitorException($"Could not find visitor class for kind: {branch.Kind}");
            }

            return type.GetConstructors().Single().Invoke(new object[] { branch }) as BaseVisitor;
        }
    }
}