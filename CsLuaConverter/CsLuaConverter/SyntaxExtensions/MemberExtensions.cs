namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Linq;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class MemberExtensions
    {
        // MemberDeclarationSyntax (multiple) http://www.coderesx.com/roslyn/html/1EB73B23.htm#fullInheritance

        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
        (syntax, textWriter, context) =>
            {
                throw new Exception($"Could not find extension method for syntax {syntax.GetType().Name}. Kind: {(syntax as CSharpSyntaxNode)?.Kind().ToString() ?? "null"}.");
            })
            .Case<ConstructorDeclarationSyntax>(Write);

        /*
        BaseFieldDeclarationSyntax
            EventFieldDeclarationSyntax
            FieldDeclarationSyntax
        BaseMethodDeclarationSyntax
      x     ConstructorDeclarationSyntax
            ConversionOperatorDeclarationSyntax
            DestructorDeclarationSyntax
            MethodDeclarationSyntax
            OperatorDeclarationSyntax
        BasePropertyDeclarationSyntax
            EventDeclarationSyntax
            IndexerDeclarationSyntax
            PropertyDeclarationSyntax
        BaseTypeDeclarationSyntax
            EnumDeclarationSyntax
            TypeDeclarationSyntax
                ClassDeclarationSyntax
                InterfaceDeclarationSyntax
                StructDeclarationSyntax
        DelegateDeclarationSyntax
        EnumMemberDeclarationSyntax
        GlobalStatementSyntax
        IncompleteMemberSyntax
        NamespaceDeclarationSyntax
             */

        public static void Write(this MemberDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

        public static void WriteEmptyConstructor(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("_M.IM(members, '', {");
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Cstor',");
            textWriter.WriteLine("static = true,");
            textWriter.WriteLine("numMethodGenerics = 0,");
            textWriter.WriteLine("signatureHash = 0,");
            textWriter.WriteLine("scope = 'Public',");
            textWriter.WriteLine("func = function(element)");

            textWriter.Indent++;
            textWriter.WriteLine("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();");
            textWriter.Indent--;

            textWriter.WriteLine("end,");

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void Write(this ConstructorInitializerSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol;

            if (syntax.Kind() == SyntaxKind.BaseConstructorInitializer)
            {
                
                textWriter.Write("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_");
                context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            }
            else if (syntax.Kind() == SyntaxKind.ThisConstructorInitializer)
            {
                textWriter.Write("(element % _M.DOT_LVL(typeObject.Level))");

                var signatureWriter = textWriter.CreateTextWriterAtSameIndent();
                var hasGenericComponents = context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), signatureWriter);

                if (hasGenericComponents)
                {
                    textWriter.Write("['_C_0_'..(");
                    textWriter.AppendTextWriter(signatureWriter);
                    textWriter.Write(")]");
                }
                else
                {
                    textWriter.Write("._C_0_");
                    textWriter.AppendTextWriter(signatureWriter);
                }
            }

            syntax.ArgumentList.Write(textWriter, context);

            textWriter.WriteLine(";");
        }

        public static void Visit(this ConstructorDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

            textWriter.WriteLine("_M.IM(members, '', {");
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Cstor',");
            textWriter.WriteLine("static = true,");
            textWriter.WriteLine("numMethodGenerics = 0,");
            textWriter.Write("signatureHash = ");
            context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            textWriter.WriteLine(",");
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);

            textWriter.Write("func = function(element");
            syntax.ParameterList.Write(textWriter, context);
            textWriter.WriteLine(")");

            textWriter.Indent++;

            if (syntax.Initializer != null)
            {
                syntax.Initializer.Write(textWriter, context);
            }
            else
            {
                textWriter.WriteLine("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();");
            }

            textWriter.Indent--;

            syntax.Body.Write(textWriter, context);

            textWriter.WriteLine("end,");
            
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }
    }
}