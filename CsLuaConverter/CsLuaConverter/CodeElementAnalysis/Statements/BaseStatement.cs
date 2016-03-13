namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseStatement : BaseElement
    {
        public static BaseStatement CreateStatement(SyntaxToken token)
        {
            var kind = token.Parent.GetKind();
            var parent = token.Parent;

            while (!kind.ToString().Contains("Statement"))
            {
                parent = parent.Parent;

                if (parent.Parent == null)
                    throw new Exception("Could not identify statement kind.");

                kind = parent.GetKind();
            }

            switch (kind)
            {
                case SyntaxKind.ReturnStatement:
                    return new ReturnStatement();
                case SyntaxKind.TryStatement:
                    return new TryStatement();
                case SyntaxKind.IfStatement:
                    return new IfStatement();
                case SyntaxKind.ForStatement:
                    return new ForStatement();
                case SyntaxKind.ForEachStatement:
                    return new ForEachStatement();
                case SyntaxKind.WhileStatement:
                    return new WhileStatement();
                case SyntaxKind.ThrowStatement:
                    return new ThrowStatement();
                case SyntaxKind.SwitchStatement:
                    return new SwitchStatement();
                case SyntaxKind.BreakStatement:
                    return new BreakStatement();
                case SyntaxKind.ExpressionStatement:
                    return new ExpressionStatement();
                case SyntaxKind.LocalDeclarationStatement:
                    return new LocalDeclarationStatement();
                default:
                    throw new Exception("Unsupported statement " + kind);
            }
        }
    }
}