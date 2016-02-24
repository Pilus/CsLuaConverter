namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using CodeElementAnalysis;
    using Providers;

    public static class VisitorList
    {
        private static readonly List<IVisitor> Visitors = new List<IVisitor>()
        {
            new NamespaceVisitor(),
            new ClassVisitor(),
            new TypeVisitor(),
            new FieldDeclarationVisitor(),
            new NumericLiteralExpressionVisitor(),
            new MethodDeclarationVisitor(),
            new ParameterListVisitor(),
            new BlockVisitor(),
            new PredefinedTypeVisitor(),
            new ParameterVisitor(),
            new IdentifierNameVisitor(),
            new GenericNameVisitor(),
            new StringLiteralExpressionVisitor(),
            new SimpleVisitors(),
            new PropertyDeclarationVisitor(),
            new InterfaceDeclarationVisitor(),
            new AttributeListVisitor(),
            new StatementVisitor(),
            new VariableDeclaratorVisitor(),
            new ObjectCreationExpressionVisitor(),
            new ArgumentListVisitor(),
            new PostIncrementExpressionVisitor(),
            new PostDecrementExpressionVisitor(),
            new ForEachStatementVisitor(),
            new BracketedArgumentListVisitor(),
            new IfStatementVisitor(),
            new TryStatementVisitor(),
            new ThrowStatementVisitor(),
            new TypeArgumentListVisitor(),
            new CollectionInitializerExpressionVisitor(),
            new EnumDeclarationVisitor(),
            new EnumMemberDeclarationVisitor(),
            new ArrayVisitor(),
            new ObjectInitializerExpressionVisitor(),
            new ComplexElementInitializerExpressionVisitor(),
            new IsExpressionVisitor(),
            new TypeOfExpressionVisitor(),
            new ConditionalExpressionVisitor(),
            new SimpleMemberAccessExpressionVisitor(),
            new ParenthesizedExpressionVisitor(),
            new BaseExpressionVisitor(),
            new ThisExpressionVisitor(),
        };

        private static IndentedTextWriter writer;
        private static IProviders providers;
        private static Type visitorType;

        [DebuggerNonUserCode]
        public static void Visit<T>(T element)
        {
            Visit<T>(element, writer, providers);
        }

        [DebuggerNonUserCode]
        public static void Visit<T>(T element, IndentedTextWriter writer, IProviders providers)
        {
            VisitorList.writer = writer;
            VisitorList.providers = providers;

            visitorType = typeof (IVisitor<>).MakeGenericType(element.GetType());
            var visitor = Visitors.SingleOrDefault(IsType);

            if (visitor == null)
            {
                throw new Exception(string.Format("No visitor found for type {0}", element.GetType().Name));
            }

            var m = visitorType.GetMethod("Visit", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);

            InvokeAndRethrow<T>(m, visitor, element);
        }

        [DebuggerNonUserCode]
        public static void WriteOpen<T>(T element)
        {
            if (element == null)
            {
                return;
            }

            visitorType = typeof(IOpenCloseVisitor<>).MakeGenericType(element.GetType());
            var visitor = Visitors.SingleOrDefault(IsType);

            if (visitor == null)
            {
                throw new Exception(string.Format("No open close visitor found for type {0}", element.GetType().Name));
            }

            var m = visitorType.GetMethod("WriteOpen", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);

            InvokeAndRethrow<T>(m, visitor, element);
        }

        [DebuggerNonUserCode]
        public static void WriteClose<T>(T element)
        {
            if (element == null)
            {
                return;
            }

            visitorType = typeof(IOpenCloseVisitor<>).MakeGenericType(element.GetType());
            var visitor = Visitors.SingleOrDefault(IsType);

            if (visitor == null)
            {
                throw new Exception(string.Format("No open close visitor found for type {0}", element.GetType().Name));
            }

            var m = visitorType.GetMethod("WriteClose", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);

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
                m.Invoke(visitor, new object[] { element, writer, providers });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}