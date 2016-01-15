namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseElement
    {
        [System.Diagnostics.DebuggerNonUserCode]
        public static void ExpectKind(SyntaxKind expectedKind, SyntaxKind actualKind)
        {
            if (!expectedKind.Equals(actualKind))
            {
                throw new Exception($"Unexpected token. Expected {expectedKind}, got {actualKind}.");
            }
        }

        [System.Diagnostics.DebuggerNonUserCode]
        public static void ExpectKind(SyntaxKind[] expectedKinds, SyntaxKind actualKind)
        {
            if (!expectedKinds.Contains(actualKind))
            {
                throw new Exception($"Unexpected token. Expected {string.Join(", ", expectedKinds)}, got {actualKind}.");
            }
        }

        private static readonly Dictionary<SyntaxKind, Func<BaseElement>> NodeMappings = new Dictionary
            <SyntaxKind, Func<BaseElement>>()
        {
            { SyntaxKind.NamespaceDeclaration,                  () => new NamespaceDeclaration() },
            { SyntaxKind.ClassDeclaration,                      () => new ClassDeclaration() },
            { SyntaxKind.IdentifierName,                        () => new IdentifierName() },
            { SyntaxKind.ConstructorDeclaration,                () => new ConstructorDeclaration() },
            { SyntaxKind.Block,                                 () => new Block() },
            { SyntaxKind.SimpleAssignmentExpression,            () => new SimpleAssignmentExpression() },
            { SyntaxKind.StringLiteralExpression,               () => new StringLiteralExpression() },
            { SyntaxKind.ThisExpression,                        () => new ThisExpression() },
            { SyntaxKind.BracketedArgumentList,                 () => new BracketedArgumentList() },
            { SyntaxKind.MethodDeclaration,                     () => new MethodDeclaration() },
            { SyntaxKind.PredefinedType,                        () => new PredefinedType() },
            { SyntaxKind.VariableDeclarator,                    () => new VariableDeclarator() },
            { SyntaxKind.EqualsValueClause,                     () => new EqualsValueClause() },
            { SyntaxKind.ObjectCreationExpression,              () => new ObjectCreationExpression() },
            { SyntaxKind.ArgumentList,                          () => new ArgumentList() },
            { SyntaxKind.SimpleMemberAccessExpression,          () => new SimpleMemberAccessExpression() },
            { SyntaxKind.NumericLiteralExpression,              () => new NumericLiteralExpression() },
            { SyntaxKind.TrueLiteralExpression,                 () => new TrueLiteralExpression() },
            { SyntaxKind.NullLiteralExpression,                 () => new NullLiteralExpression() },
            { SyntaxKind.Parameter,                             () => new Parameter() },
            { SyntaxKind.FieldDeclaration,                      () => new FieldDeclaration() },
            { SyntaxKind.GenericName,                           () => new GenericName() },
            { SyntaxKind.UsingDirective,                        () => new UsingDirective() },
            { SyntaxKind.TypeParameter,                         () => new TypeParameter() },
            { SyntaxKind.ArrayCreationExpression,               () => new ArrayCreationExpression() },
            { SyntaxKind.ImplicitArrayCreationExpression,       () => new ImplicitArrayCreationExpression() },
            { SyntaxKind.ObjectInitializerExpression,           () => new ObjectInitializerExpression() },
            { SyntaxKind.ReturnStatement,                       () => new ReturnStatement() },
            { SyntaxKind.PropertyDeclaration,                   () => new PropertyDeclaration() },
            { SyntaxKind.IfStatement,                           () => new IfStatement() },
            { SyntaxKind.IsExpression,                          () => new IsExpression() },
            { SyntaxKind.ParenthesizedExpression,               () => new ParenthesizedExpression() },
            { SyntaxKind.AsExpression,                          () => new AsExpression() },
            { SyntaxKind.EqualsExpression,                      () => new EqualsExpression() },
            { SyntaxKind.BaseExpression,                        () => new BaseExpression() },
            { SyntaxKind.AddExpression,                         () => new AddExpression() },
            { SyntaxKind.EnumDeclaration,                       () => new EnumDeclaration() },
            { SyntaxKind.EnumMemberDeclaration,                 () => new EnumMemberDeclaration() },
            { SyntaxKind.InterfaceDeclaration,                  () => new InterfaceDeclaration() },
            { SyntaxKind.CastExpression,                        () => new CastExpression() },
            { SyntaxKind.FalseLiteralExpression,                () => new FalseLiteralExpression() },
            { SyntaxKind.PostIncrementExpression,               () => new PostIncrementExpression() },
            { SyntaxKind.ForEachStatement,                      () => new ForEachStatement() },
            { SyntaxKind.TryStatement,                          () => new TryStatement() },
            { SyntaxKind.CharacterLiteralExpression,            () => new CharacterLiteralExpression() },
            { SyntaxKind.PostDecrementExpression,               () => new PostDecrementExpression() },
            { SyntaxKind.ElseClause,                            () => new ElseClause() },
            { SyntaxKind.NotEqualsExpression,                   () => new NotEqualsExpression() },
            { SyntaxKind.ThrowStatement,                        () => new ThrowStatement() },
            { SyntaxKind.ParameterList,                         () => new ParameterList() },
            { SyntaxKind.ParenthesizedLambdaExpression,         () => new ParenthesizedLambdaExpression() },
            { SyntaxKind.SubtractAssignmentExpression,          () => new SubtractAssignmentExpression() },
            { SyntaxKind.AddAssignmentExpression,               () => new AddAssignmentExpression() },
            { SyntaxKind.MultiplyExpression,                    () => new MultiplyExpression() },
            { SyntaxKind.AttributeList,                         () => new AttributeList() },
            { SyntaxKind.NameEquals,                            () => new NameEquals() },
            { SyntaxKind.CollectionInitializerExpression,       () => new CollectionInitializerExpression() },
            { SyntaxKind.SimpleLambdaExpression,                () => new SimpleLambdaExpression() },
            { SyntaxKind.ComplexElementInitializerExpression,   () => new ComplexElementInitializerExpression() },
            { SyntaxKind.TypeOfExpression,                      () => new TypeOfExpression() },
            { SyntaxKind.LogicalNotExpression,                  () => new LogicalNotExpression() },
            { SyntaxKind.ConditionalExpression,                 () => new ConditionalExpression() },
            { SyntaxKind.SubtractExpression,                    () => new SubtractExpression() },
        };

        public abstract SyntaxToken Analyze(SyntaxToken token);

        public static BaseElement GenerateMatchingElement(SyntaxToken token)
        {
            var mapping = NodeMappings.FirstOrDefault(m => token.Parent.IsKind(m.Key)).Value;
            if (mapping == null)
            {
                throw new Exception($"Could not find a mapping for parent syntax kind. {token.Parent.GetKind()}");
            }

            return mapping();
        }
    }
}