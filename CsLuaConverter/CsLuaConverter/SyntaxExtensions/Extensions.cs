namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class Extensions
    {
        public static void Write<T>(this SeparatedSyntaxList<T> list, Action<T, IIndentedTextWriterWrapper, IContext> action, IIndentedTextWriterWrapper textWriter, IContext context) where T : CSharpSyntaxNode
        {
            for (var index = 0; index < list.Count; index++)
            {
                action(list[index], textWriter, context);

                if (index != list.Count - 1)
                {
                    textWriter.Write(", ");
                }
            }
        }

        public static void Write<T>(this SyntaxList<T> list, Action<T, IIndentedTextWriterWrapper, IContext> action, IIndentedTextWriterWrapper textWriter, IContext context) where T : CSharpSyntaxNode
        {
            foreach (T syntax in list)
            {
                action(syntax, textWriter, context);
            }
        }

        public static void Write(this AccessorDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Body == null)
            {
                return;
            }

            textWriter.Write(syntax.Keyword.Text);
            textWriter.Write(" = function(element");

            var indexerDeclaration = syntax.Parent.Parent as IndexerDeclarationSyntax;
            if (indexerDeclaration != null)
            {
                textWriter.Write(", ");
                indexerDeclaration.ParameterList.Parameters.Write(Write, textWriter, context);
            }

            if (syntax.Keyword.Kind() == SyntaxKind.SetKeyword)
            {
                textWriter.Write(", value");
            }

            textWriter.WriteLine(")");
            syntax.Body.Write(textWriter, context);
            textWriter.WriteLine("end,");
        }

        public static void Write(this ParameterSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Modifiers.Any(mod => mod.Kind() == SyntaxKind.ParamsKeyword))
            {
                textWriter.Write("firstParam, ...");
            }
            else
            {
                textWriter.Write(syntax.Identifier.Text);
            }
        }

        private static readonly TypeSwitch StatementTypeSwitch = new TypeSwitch(obj =>
        {
            throw new Exception($"Could not find extension method for statementSyntax {obj.GetType().Name}.");
        })
            .Case<BlockSyntax>(Write);

        /*
        BreakStatementSyntax
        CheckedStatementSyntax
        ContinueStatementSyntax
        DoStatementSyntax
        EmptyStatementSyntax
        ExpressionStatementSyntax
        FixedStatementSyntax
        ForEachStatementSyntax
        ForStatementSyntax
        GotoStatementSyntax
        IfStatementSyntax
        LabeledStatementSyntax
        LocalDeclarationStatementSyntax
        LockStatementSyntax
        ReturnStatementSyntax
        SwitchStatementSyntax
        ThrowStatementSyntax
        TryStatementSyntax
        UnsafeStatementSyntax
        UsingStatementSyntax
        WhileStatementSyntax
        YieldStatementSyntax
        */

        public static void Write(this StatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            StatementTypeSwitch.Write(syntax, textWriter, context);
        }

        public static void Write(this BlockSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Indent++;
            syntax.Statements.Write(Write, textWriter, context);
            textWriter.Indent--;
        }

    }
}