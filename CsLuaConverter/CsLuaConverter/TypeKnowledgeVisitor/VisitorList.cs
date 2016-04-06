namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using CodeElementAnalysis;
    using Providers;

    public static class VisitorList
    {
        private static readonly DefaultContainerVisitor DefaultContainerVisitor = new DefaultContainerVisitor();
        private static readonly DefaultDelimiteredContainerElementVisitor DefaultDelimiteredContainerVisitor = new DefaultDelimiteredContainerElementVisitor();
        private static readonly List<IVisitor> Visitors = new List<IVisitor>()
        {
            new ParameterListVisitor(),
            new ConstructorDeclarationVisitor(),
        };

        private static IProviders providers;
        private static Type visitorType;

        [DebuggerNonUserCode]
        public static void Visit<T>(T element)
        {
            Visit<T>(element, providers);
        }

        [DebuggerNonUserCode]
        public static void Visit<T>(T element, IProviders providers)
        {
            VisitorList.providers = providers;

            visitorType = typeof (IVisitor<>).MakeGenericType(element.GetType());
            var visitor = Visitors.SingleOrDefault(IsType);

            if (visitor == null)
            {
                if (element is DelimiteredContainerElement)
                {
                    DefaultDelimiteredContainerVisitor.Visit(element as DelimiteredContainerElement, providers);
                }
                else if (element is ContainerElement)
                {
                    DefaultContainerVisitor.Visit(element as ContainerElement, providers);
                }

                return;
            }

            var m = visitorType.GetMethod("Visit", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);

            InvokeAndRethrow<T>(m, visitor, element);
        }

        [DebuggerNonUserCode]
        private static bool IsType(IVisitor visitor)
        {
            return visitorType.IsInstanceOfType(visitor);
        }

        [DebuggerNonUserCode]
        private static void InvokeAndRethrow<T>(MethodInfo m, IVisitor visitor, T element)
        {
            try
            {
                m.Invoke(visitor, new object[] { element, providers });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}