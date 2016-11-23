namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression;
    using CsLuaConverter.Context;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ExpressionExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            (syntax, textWriter, context) =>
                {
                    SyntaxVisitorBase<MemberAccessExpressionSyntax>.VisitNode((CSharpSyntaxNode)syntax, textWriter, context);
                    //throw new Exception($"Could not find extension method for expressionSyntax {obj.GetType().Name}.");
                })
            .Case<AssignmentExpressionSyntax>(Write)
            .Case<MemberAccessExpressionSyntax>(Write)
            .Case<TypeSyntax>(TypeExtensions.Write);

        /*
        AnonymousFunctionExpressionSyntax
        AnonymousObjectCreationExpressionSyntax
        ArrayCreationExpressionSyntax
        AssignmentExpressionSyntax
        AwaitExpressionSyntax
        BinaryExpressionSyntax
        CastExpressionSyntax
        CheckedExpressionSyntax
        ConditionalAccessExpressionSyntax
        ConditionalExpressionSyntax
        DefaultExpressionSyntax
        ElementAccessExpressionSyntax
        ElementBindingExpressionSyntax
        ImplicitArrayCreationExpressionSyntax
        ImplicitElementAccessSyntax
        InitializerExpressionSyntax
        InstanceExpressionSyntax
        InterpolatedStringExpressionSyntax
        InvocationExpressionSyntax
        LiteralExpressionSyntax
        MakeRefExpressionSyntax
        MemberBindingExpressionSyntax
        ObjectCreationExpressionSyntax
        OmittedArraySizeExpressionSyntax
        ParenthesizedExpressionSyntax
        PostfixUnaryExpressionSyntax
        PrefixUnaryExpressionSyntax
        QueryExpressionSyntax
        RefTypeExpressionSyntax
        RefValueExpressionSyntax
        SizeOfExpressionSyntax
        StackAllocArrayCreationExpressionSyntax
        TypeOfExpressionSyntax
        */

        public static void Write(this ExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

        public static void Write(this AssignmentExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            string prefix = "";
            string delimiter = "";
            string suffix = "";
            bool skipRepeatOfLeft = false;

            switch (syntax.Kind())
            {
                case SyntaxKind.SimpleAssignmentExpression:
                    skipRepeatOfLeft = true;
                    break;
                case SyntaxKind.AddAssignmentExpression:
                    delimiter = " +_M.Add+ ";
                    break;
                case SyntaxKind.AndAssignmentExpression:
                    prefix = "bit.band(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.DivideAssignmentExpression:
                    delimiter = " / ";
                    break;
                case SyntaxKind.ExclusiveOrAssignmentExpression:
                    prefix = "bit.bxor(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.LeftShiftAssignmentExpression:
                    prefix = "bit.lshift(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.ModuloAssignmentExpression:
                    prefix = "math.mod(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.MultiplyAssignmentExpression:
                    delimiter = " * ";
                    break;
                case SyntaxKind.OrAssignmentExpression:
                    prefix = "bit.bor(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.RightShiftAssignmentExpression:
                    prefix = "bit.rshift(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.SubtractAssignmentExpression:
                    delimiter = " - ";
                    break;
                default:
                    throw new Exception($"Unknown assignment expression kind: {syntax.Kind()}.");
            }

            syntax.Left.Write(textWriter, context);
            textWriter.Write(" = ");

            textWriter.Write(prefix);
            if (!skipRepeatOfLeft)
            {
                syntax.Left.Write(textWriter, context);
                textWriter.Write(delimiter);
            }
            
            syntax.Right.Write(textWriter, context);
            textWriter.Write(suffix);
        }


        public static void Write(this MemberAccessExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Expression == null)
            {
                syntax.Name.Write(textWriter, context);
                return;
            }

            if (!(syntax.Expression is ThisExpressionSyntax || syntax.Expression is BaseExpressionSyntax))
            {
                textWriter.Write("(");
                WriteAccessExpression(syntax, textWriter, context);
                textWriter.Write(" % _M.DOT).");
            }

            syntax.Name.Write(textWriter, context);
        }

        private static void WriteAccessExpression(MemberAccessExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(syntax).Symbol;

            if (symbol == null)
            {
                syntax.Expression.Write(textWriter, context);
                return;
            }

            if (symbol is ITypeSymbol)
            {
                context.TypeReferenceWriter.WriteInteractionElementReference((ITypeSymbol)symbol, textWriter);
                return;
            }

            if (!symbol.IsStatic)
            {
                syntax.Expression.Write(textWriter, context);
                return;
            }

            context.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);
        }
    }
}