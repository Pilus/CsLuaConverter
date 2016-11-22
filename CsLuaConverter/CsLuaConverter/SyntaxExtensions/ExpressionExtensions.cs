namespace CsLuaConverter.SyntaxExtensions
{
    using System;

    using CsLuaConverter.Context;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ExpressionExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            obj =>
                {
                    throw new Exception($"Could not find extension method for expressionSyntax {obj.GetType().Name}.");
                })
            .Case<AssignmentExpressionSyntax>(Write);
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
        MemberAccessExpressionSyntax
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
        TypeSyntax
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

            switch (syntax.Kind())
            {
                case SyntaxKind.AddAssignmentExpression:
                    delimiter = " +_M.Add+ ";
                    break;
                case SyntaxKind.AndAssignmentExpression:
                    prefix = "bit.band(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.DivideAssignmentExpression:
                    delimiter = " / ";
                    break;
                case SyntaxKind.ExclusiveOrAssignmentExpression:
                    prefix = "bit.bxor(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.LeftShiftAssignmentExpression:
                    prefix = "bit.lshift(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.ModuloAssignmentExpression:
                    prefix = "math.mod(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.MultiplyAssignmentExpression:
                    delimiter = " / ";
                    break;
                case SyntaxKind.OrAssignmentExpression:
                    prefix = "bit.bor(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.RightShiftAssignmentExpression:
                    prefix = "bit.rshift(";
                    delimiter = ",";
                    suffix = ")";
                    break;
                case SyntaxKind.SubtractAssignmentExpression:
                    delimiter = " - ";
                    break;
            }

            syntax.Left.Write(textWriter, context);
            textWriter.Write(" = ");

            textWriter.Write(prefix);
            syntax.Left.Write(textWriter, context);
            textWriter.Write(delimiter);
            syntax.Right.Write(textWriter, context);
            textWriter.Write(suffix);
        }
    }
}