namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using CodeTree;
    using System.Linq;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseVisitor : IVisitor
    {
        private readonly static System.Type[] baseVisitorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(BaseVisitor))).ToArray();

        protected readonly CodeTreeBranch Branch;

        protected BaseVisitor(CodeTreeBranch branch)
        {
            Branch = branch;
        }

        public abstract void Visit(IIndentedTextWriterWrapper textWriter, IContext context);

        [DebuggerNonUserCode]
        protected static bool IsKind(CodeTreeNode node, SyntaxKind kind)
        {
            return node.Kind == kind;
        }

        private Func<CodeTreeBranch, BaseVisitor> customFactory;
        [DebuggerNonUserCode]
        protected BaseVisitor[] CreateVisitors(INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            this.customFactory = customFactory;
            return this.GetFilteredNodes(filter).OfType<CodeTreeBranch>()
                .Select(this.CreateVisitorWithCustomFactory)
                .ToArray();
        }

        [DebuggerNonUserCode]
        private BaseVisitor CreateVisitorWithCustomFactory(CodeTreeBranch branch)
        {
            return this.customFactory?.Invoke(branch) ?? CreateVisitor(branch);
        }

        [DebuggerNonUserCode]
        protected CodeTreeNode[] GetFilteredNodes(INodeFilter filter = null)
        {
            return (filter == null ? this.Branch.Nodes : filter.Filter(this.Branch.Nodes)).ToArray();
        }

        protected void CreateVisitorsAndVisitBranches(IIndentedTextWriterWrapper textWriter, IContext context, INodeFilter filter = null, Func<CodeTreeBranch, BaseVisitor> customFactory = null)
        {
            foreach (var visitor in this.CreateVisitors(filter, customFactory))
            {
                visitor.Visit(textWriter, context);
            }
        }

        [DebuggerNonUserCode]
        protected void ExpectKind(int index, params SyntaxKind[] kinds)
        {
            if (!kinds.Contains(this.Branch.Nodes[index].Kind))
            {
                throw new VisitorException($"In {this.Branch.Kind}: Expected kind {string.Join(", ", kinds)} at index {index}. Got {this.Branch.Nodes[index].Kind}.");
            }
        }

        [DebuggerNonUserCode]
        protected bool IsKind(int index, SyntaxKind kind)
        {
            return index < this.Branch.Nodes.Length && this.Branch.Nodes[index].Kind == kind;
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
                var constructor = type.GetConstructors().SingleOrDefault();

                if (constructor == null)
                {
                    throw new VisitorException($"Visitor for kind: {branch.Kind} does not implemet any constructors");
                }

                return constructor.Invoke(new object[] { branch }) as BaseVisitor;
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