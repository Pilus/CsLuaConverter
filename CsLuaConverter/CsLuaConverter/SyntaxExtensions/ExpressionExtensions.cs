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

    public static class ExpressionExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            (syntax, textWriter, context) =>
                {
                    SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode((CSharpSyntaxNode)syntax, textWriter, context);
                    //throw new Exception($"Could not find extension method for expressionSyntax {syntax.GetType().Name}. Kind: {(syntax as CSharpSyntaxNode)?.Kind().ToString() ?? "null"}.");
                })
            .Case<AssignmentExpressionSyntax>(Write)
            .Case<MemberAccessExpressionSyntax>(Write)
            .Case<TypeSyntax>(TypeExtensions.Write)
            .Case<ObjectCreationExpressionSyntax>(Write)
            .Case<NameSyntax>(NameExtensions.Write)
            .Case<InvocationExpressionSyntax>(Write)
            .Case<LiteralExpressionSyntax>(Write)
            .Case<BinaryExpressionSyntax>(Write)
            .Case<LambdaExpressionSyntax>(Write)
            .Case<ParenthesizedExpressionSyntax>(Write);

        /*
        AnonymousFunctionExpressionSyntax
        AnonymousObjectCreationExpressionSyntax
        ArrayCreationExpressionSyntax
        AssignmentExpressionSyntax
        AwaitExpressionSyntax
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
        MakeRefExpressionSyntax
        MemberBindingExpressionSyntax
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

        public static void Write(this ObjectCreationExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol;

            textWriter.Write(syntax.Initializer != null ? "(" : "");


            ITypeSymbol[] parameterTypes = null;
            IDictionary<ITypeSymbol, ITypeSymbol> appliedClassGenerics = null;
            if (symbol != null)
            {
                context.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);
                parameterTypes = symbol.OriginalDefinition.Parameters.Select(p => p.Type).ToArray();
                appliedClassGenerics = ((TypeSymbolSemanticAdaptor) context.SemanticAdaptor).GetAppliedClassGenerics(symbol.ContainingType);
            }
            else
            {
                // Special case for missing symbol. Roslyn issue 3825. https://github.com/dotnet/roslyn/issues/3825
                var namedTypeSymbol = (INamedTypeSymbol)context.SemanticModel.GetSymbolInfo(syntax.Type).Symbol;
                context.TypeReferenceWriter.WriteInteractionElementReference(namedTypeSymbol, textWriter);

                if (namedTypeSymbol.TypeKind != TypeKind.Delegate)
                {
                    throw new Exception($"Could not guess constructor for {namedTypeSymbol}.");
                }

                parameterTypes = new ITypeSymbol[] { namedTypeSymbol };
            }

            var signatureWiter = textWriter.CreateTextWriterAtSameIndent();
            var hasGenricComponents = context.SignatureWriter.WriteSignature(parameterTypes, signatureWiter, appliedClassGenerics);

            if (hasGenricComponents)
            {
                textWriter.Write("['_C_0_'..");
            }
            else
            {
                textWriter.Write("._C_0_");
            }

            textWriter.AppendTextWriter(signatureWiter);

            if (hasGenricComponents)
            {
                textWriter.Write("]");
            }

            SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode(syntax.ArgumentList, textWriter, context);

            if (syntax.Initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode(syntax.Initializer, textWriter, context);
            }
        }

        private static readonly string[] namespacesWithNoAmbigiousMethods = new [] {"Lua"};

        public static void Write(this InvocationExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IMethodSymbol) ModelExtensions.GetSymbolInfo(context.SemanticModel, syntax).Symbol;

            if (symbol.IsExtensionMethod && symbol.MethodKind == MethodKind.ReducedExtension)
            {
                WriteAsExtensionMethodCall(syntax, textWriter, context, symbol);
                return;
            }

            textWriter.Write("(");

            if (symbol.MethodKind != MethodKind.DelegateInvoke)
            {
                var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
                var signatureHasGenerics =
                    context.SignatureWriter.WriteSignature(symbol.ConstructedFrom.Parameters.Select(p => p.Type).ToArray(),
                        signatureTextWriter);

                if (signatureHasGenerics)
                {
                    var targetWriter = textWriter.CreateTextWriterAtSameIndent();
                    syntax.Expression.Write(targetWriter, context);

                    var expectedEnd = $".{symbol.Name}.";
                    if (targetWriter.ToString().EndsWith(expectedEnd))
                    {
                        throw new Exception($"Expect index visitor to end with '{expectedEnd}'. Got '{targetWriter}'");
                    }

                    var targetString = targetWriter.ToString();
                    textWriter.Write(targetString.Remove(targetString.Length - expectedEnd.Length + 1));
                    textWriter.Write($"['{symbol.Name}");
                }
                else
                {
                    syntax.Expression.Write(textWriter, context);
                }

                var fullNamespace = context.SemanticAdaptor.GetFullNamespace(symbol.ContainingType);
                if (!namespacesWithNoAmbigiousMethods.Contains(fullNamespace))
                {
                    textWriter.Write("_M_{0}_", symbol.TypeArguments.Length);

                    if (signatureHasGenerics)
                    {
                        textWriter.Write("'..(");
                    }

                    textWriter.AppendTextWriter(signatureTextWriter);

                    if (signatureHasGenerics)
                    {
                        textWriter.Write(")]");
                    }
                }
            }
            else
            {
                syntax.Expression.Write(textWriter, context);
            }

            if (symbol.TypeArguments.Any())
            {
                context.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            SyntaxVisitorBase<InvocationExpressionSyntax>.VisitNode(syntax.ArgumentList, textWriter, context);
        }

        private static void WriteAsExtensionMethodCall(InvocationExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context,
            IMethodSymbol symbol)
        {
            textWriter.Write("(({0} % _M.DOT).", context.SemanticAdaptor.GetFullName(symbol.ContainingType));

            var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
            var signatureHasGenerics = context.SignatureWriter.WriteSignature(symbol.ReducedFrom.Parameters.Select(p => p.Type).ToArray(), signatureTextWriter);

            if (signatureHasGenerics)
            {
                textWriter.Write("['");
            }
            
            textWriter.Write("{0}_M_{1}_", symbol.Name, symbol.TypeArguments.Length);

            if (signatureHasGenerics)
            {
                textWriter.Write("'..(");
            }

            textWriter.AppendTextWriter(signatureTextWriter);

            if (signatureHasGenerics)
            {
                textWriter.Write(")]");
            }

            if (symbol.TypeArguments.Any())
            {
                context.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            var argWriter = textWriter.CreateTextWriterAtSameIndent();
            SyntaxVisitorBase<InvocationExpressionSyntax>.VisitNode(syntax.ArgumentList, argWriter, context);

            var targetWriter = textWriter.CreateTextWriterAtSameIndent();
            syntax.Expression.Write(targetWriter, context);
            var targetStr = targetWriter.ToString();
            textWriter.Write(targetStr.Substring(0, targetStr.LastIndexOf(" % _M.DOT)")));

            var argStr = argWriter.ToString();
            if (argStr.Length > 2)
            {
                textWriter.Write(", ");
            }
            
            textWriter.Write(argStr.Substring(1)); // Skip the opening (
        }

        public static void Write(this LiteralExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var text = syntax.Token.Text;
            switch (syntax.Kind())
            {
                case SyntaxKind.NullLiteralExpression:
                    text = "nil";
                    break;
                case SyntaxKind.StringLiteralExpression:
                    if (text.StartsWith("@"))
                    {
                        text = "[[" + text.Substring(2, text.Length - 3) + "]]";
                    }
                    break;
            }

            textWriter.Write(text);
        }

        public static void Write(this BinaryExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var prefix = "";
            var delimiter = "";
            var suffix = "";

            switch (syntax.Kind())
            {
                case SyntaxKind.AddExpression:
                    delimiter = " +_M.Add+ ";
                    break;
                case SyntaxKind.AsExpression:
                    syntax.Left.Write(textWriter, context);
                    return;
                case SyntaxKind.BitwiseAndExpression:
                    prefix = "bit.band(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.BitwiseOrExpression:
                    prefix = "bit.bor(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.CoalesceExpression:
                    delimiter = " or ";
                    break;
                case SyntaxKind.DivideExpression:
                    delimiter = " / ";
                    break;
                case SyntaxKind.EqualsExpression:
                    delimiter = " == ";
                    break;
                case SyntaxKind.ExclusiveOrExpression:
                    prefix = "bit.bxor(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.GreaterThanExpression:
                    delimiter = " > ";
                    break;
                case SyntaxKind.GreaterThanOrEqualExpression:
                    delimiter = " >= ";
                    break;
                case SyntaxKind.IsExpression:
                    var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(syntax.Right).Symbol;
                    context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
                    textWriter.Write(".__is(");
                    syntax.Left.Write(textWriter, context);
                    textWriter.Write(")");
                    return;
                case SyntaxKind.LeftShiftExpression:
                    prefix = "bit.lshift(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.LessThanExpression:
                    delimiter = " < ";
                    break;
                case SyntaxKind.LessThanOrEqualExpression:
                    delimiter = " <= ";
                    break;
                case SyntaxKind.LogicalAndExpression:
                    delimiter = " and ";
                    break;
                case SyntaxKind.LogicalOrExpression:
                    delimiter = " or ";
                    break;
                case SyntaxKind.ModuloExpression:
                    prefix = "math.mod(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.MultiplyExpression:
                    delimiter = " * ";
                    break;
                case SyntaxKind.NotEqualsExpression:
                    delimiter = " ~= ";
                    break;
                case SyntaxKind.RightShiftExpression:
                    prefix = "bit.rshift(";
                    delimiter = ", ";
                    suffix = ")";
                    break;
                case SyntaxKind.SubtractExpression:
                    delimiter = " - ";
                    break;
                default:
                    throw new Exception($"Unknown binary expression syntax kind {syntax.Kind()}.");
            }

            textWriter.Write(prefix);
            syntax.Left.Write(textWriter, context);
            textWriter.Write(delimiter);
            syntax.Right.Write(textWriter, context);
            textWriter.Write(suffix);
        }

        public static void Write(LambdaExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context) { 

            var symbol = GetSymbolForParentUsingTheLambda(syntax, context);
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
            textWriter.Write("._C_0_16704"); // Lua.Function as argument
            textWriter.Write("(function(");

            if (syntax is SimpleLambdaExpressionSyntax)
            {
                ((SimpleLambdaExpressionSyntax)syntax).Parameter.Write(textWriter, context);
            }
            else
            {
                ((ParenthesizedLambdaExpressionSyntax)syntax).ParameterList.Write(textWriter, context);
            }

            textWriter.Write(")");

            if (syntax.Body is BlockSyntax)
            {
                textWriter.WriteLine("");
                syntax.Body.Write(textWriter, context);
            }
            else
            {
                if (!(syntax.Body is AssignmentExpressionSyntax))
                {
                    textWriter.Write(" return ");
                }

                syntax.Body.Write(textWriter, context);
                textWriter.Write(" ");
            }

            textWriter.Write("end)");
        }

        private static ITypeSymbol GetSymbolForParentUsingTheLambda(LambdaExpressionSyntax syntax, IContext context)
        {
            var argListSyntax = syntax.Ancestors().OfType<ArgumentListSyntax>().First();

            if (argListSyntax != null)
            {
                var argument = syntax.Ancestors().OfType<ArgumentSyntax>().First();
                var argNum = argListSyntax.ChildNodes().ToList().IndexOf(argument);

                if (argListSyntax.Parent is InvocationExpressionSyntax)
                {
                    var symbol = (IMethodSymbol)ModelExtensions.GetSymbolInfo(context.SemanticModel, (InvocationExpressionSyntax) argListSyntax.Parent).Symbol;
                    return symbol.Parameters[argNum].Type;
                }
                else if (argListSyntax.Parent is ObjectCreationExpressionSyntax)
                {
                    var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(argListSyntax.Parent).Symbol;

                    if (symbol != null)
                    {
                        throw new NotImplementedException();
                        // Note: not verified code path
                        return symbol.Parameters[argNum].Type;
                    }

                    var namedTypeSymbol = (INamedTypeSymbol)ModelExtensions.GetSymbolInfo(context.SemanticModel, (argListSyntax.Parent as ObjectCreationExpressionSyntax).Type).Symbol;

                    if (namedTypeSymbol.TypeKind == TypeKind.Delegate)
                    {
                        return namedTypeSymbol;
                    }

                    throw new Exception($"Could not guess constructor for {namedTypeSymbol}.");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void Write(ParenthesizedExpressionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("(");
            syntax.Expression.Write(textWriter, context);
            textWriter.Write(")");
        }
    }
}