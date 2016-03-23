namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.Reflection;
    using CodeTree;
    using Providers;
    using System.Linq;
    using Filters;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseVisitor : IVisitor
    {
        private readonly static System.Type[] baseVisitorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(BaseVisitor))).ToArray();

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

        [DebuggerNonUserCode]
        protected static bool IsKind(CodeTreeNode node, SyntaxKind kind)
        {
            return node.Kind == kind;
        }

        [DebuggerNonUserCode]
        protected BaseVisitor[] CreateVisitors(INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            return this.GetFilteredNodes(filter).OfType<CodeTreeBranch>()
                .Select(branch => customFactory?.Invoke(branch) ?? CreateVisitor(branch))
                .ToArray();
        }

        [DebuggerNonUserCode]
        protected CodeTreeNode[] GetFilteredNodes(INodeFilter filter = null)
        {
            return (filter == null ? this.Branch.Nodes : filter.Filter(this.Branch.Nodes)).ToArray();
        }

        protected void CreateVisitorsAndVisitBranches(IndentedTextWriter textWriter, IProviders providers, INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            foreach (var visitor in this.CreateVisitors(filter, customFactory))
            {
                visitor.Visit(textWriter, providers);
            }
        }

        [DebuggerNonUserCode]
        protected void ExpectKind(int index, params SyntaxKind[] kinds)
        {
            if (!kinds.Contains(this.Branch.Nodes[index].Kind))
            {
                throw new VisitorException($"Expected kind {string.Join(", ", kinds)} at index {index}. Got {this.Branch.Nodes[index].Kind}.");
            }
        }

        [DebuggerNonUserCode]
        protected BaseVisitor CreateVisitor(int index)
        {
            var branch = this.Branch.Nodes[index] as CodeTreeBranch;

            if (branch == null)
            {
                throw new VisitorException($"Could not find a branch at index {index}.");
            }

            return CreateVisitor(branch);
        }


        public static bool LockVisitorCreation { get; set; }

        [DebuggerNonUserCode]
        protected static BaseVisitor CreateVisitor(CodeTreeBranch branch)
        {
            if (LockVisitorCreation)
            {
                throw new VisitorException($"Visitor creation have been locked. All visitors must be created before visiting begins.");
            }

            var type = baseVisitorTypes.SingleOrDefault(t => t.Name.Equals(branch.Kind.ToString() + "Visitor"));

            if (type == null)
            {
                throw new VisitorException($"Could not find visitor class for kind: {branch.Kind}");
            }

            try
            {
                return type.GetConstructors().Single().Invoke(new object[] { branch }) as BaseVisitor;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }


        [DebuggerNonUserCode]
        protected static void TryActionAndWrapException(Action action, string wrapperExceptionText)
        {
            if (Debugger.IsAttached)
            {
                action.Invoke();
                return;
            }

            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {

                throw new WrappingException(wrapperExceptionText, ex);
            }
        }
    }
}