namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Linq;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class SyntaxNodeExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            (syntax, textWriter, context) =>
            {
                SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode((CSharpSyntaxNode) syntax, textWriter, context);
                //throw new Exception($"Could not find extension method for syntax {syntax.GetType().Name}. Kind: {(syntax as CSharpSyntaxNode)?.Kind().ToString() ?? "null"}.");
            });
        /*
        AccessorListSyntax
        AnonymousObjectMemberDeclaratorSyntax
        ArrayRankSpecifierSyntax
        ArrowExpressionClauseSyntax
        AttributeArgumentListSyntax
        AttributeArgumentSyntax
        AttributeListSyntax
        AttributeSyntax
        AttributeTargetSpecifierSyntax
        BaseArgumentListSyntax (multiple) http://www.coderesx.com/roslyn/html/CD5162BE.htm
        BaseCrefParameterListSyntax (multiple) http://www.coderesx.com/roslyn/html/8DFF3B4E.htm
        BaseListSyntax
        BaseParameterListSyntax (multiple) http://www.coderesx.com/roslyn/html/E5C82C37.htm
        BaseTypeSyntax
        CatchClauseSyntax
        CatchDeclarationSyntax
        CatchFilterClauseSyntax
        CompilationUnitSyntax
        ConstructorInitializerSyntax
        CrefParameterSyntax
        CrefSyntax
        ElseClauseSyntax
        EqualsValueClauseSyntax
        ExplicitInterfaceSpecifierSyntax
        ExpressionSyntax
        ExternAliasDirectiveSyntax
        FinallyClauseSyntax
        InterpolatedStringContentSyntax (multiple) http://www.coderesx.com/roslyn/html/21FD763E.htm
        InterpolationAlignmentClauseSyntax
        InterpolationFormatClauseSyntax
        JoinIntoClauseSyntax
        MemberDeclarationSyntax (multiple) http://www.coderesx.com/roslyn/html/1EB73B23.htm#fullInheritance
        NameColonSyntax
        NameEqualsSyntax
        OrderingSyntax
        QueryBodySyntax
        QueryClauseSyntax (multiple) http://www.coderesx.com/roslyn/html/653AF424.htm
        QueryContinuationSyntax
        SelectOrGroupClauseSyntax (multiple) http://www.coderesx.com/roslyn/html/22E3D638.htm
        StructuredTriviaSyntax (multiple) http://www.coderesx.com/roslyn/html/95F4125E.htm
        SwitchLabelSyntax (multiple) http://www.coderesx.com/roslyn/html/9DA2DA90.htm
        SwitchSectionSyntax
        TypeArgumentListSyntax
        TypeParameterConstraintClauseSyntax
        TypeParameterConstraintSyntax (multiple) http://www.coderesx.com/roslyn/html/4B6E21F4.htm
        TypeParameterListSyntax
        TypeParameterSyntax
        UsingDirectiveSyntax
        VariableDeclarationSyntax
        VariableDeclaratorSyntax
        XmlAttributeSyntax (multiple) http://www.coderesx.com/roslyn/html/C79783A1.htm
        XmlElementEndTagSyntax
        XmlElementStartTagSyntax
        XmlNameSyntax
        XmlNodeSyntax
        XmlPrefixSyntax
        */

        public static void Write(this CSharpSyntaxNode syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

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

        public static void Write(this ArgumentSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            syntax.Expression.Write(textWriter, context);
        }
    }
}