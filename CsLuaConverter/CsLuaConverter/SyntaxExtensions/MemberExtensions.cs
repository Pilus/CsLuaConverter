namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Linq;
    using CsLuaConverter.CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Member;
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
            .Case<ConstructorDeclarationSyntax>(Write)
            .Case<FieldDeclarationSyntax>(Write)
            .Case<PropertyDeclarationSyntax>(Write)
            .Case<IndexerDeclarationSyntax>(Write)
            .Case<MethodDeclarationSyntax>(MethodExtensions.Write)
            .Case<ClassDeclarationSyntax>(ClassExtensions.Write)
            .Case<InterfaceDeclarationSyntax>(InterfaceExtensions.Write)
            .Case<EnumDeclarationSyntax>(Write)
            .Case<NamespaceDeclarationSyntax>(Write);

        /*
        BaseFieldDeclarationSyntax
            EventFieldDeclarationSyntax
      x     FieldDeclarationSyntax
        BaseMethodDeclarationSyntax
      x     ConstructorDeclarationSyntax
            ConversionOperatorDeclarationSyntax
            DestructorDeclarationSyntax
      x     MethodDeclarationSyntax
            OperatorDeclarationSyntax
        BasePropertyDeclarationSyntax
            EventDeclarationSyntax
      x     IndexerDeclarationSyntax
      x     PropertyDeclarationSyntax
        BaseTypeDeclarationSyntax
      x     EnumDeclarationSyntax
            TypeDeclarationSyntax
      x         ClassDeclarationSyntax
      x         InterfaceDeclarationSyntax
                StructDeclarationSyntax
        DelegateDeclarationSyntax
      x EnumMemberDeclarationSyntax
        GlobalStatementSyntax
        IncompleteMemberSyntax
        NamespaceDeclarationSyntax
             */

        public static void Write(this MemberDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

        public static int GetNumGenerics(this MemberDeclarationSyntax syntax, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax) as INamedTypeSymbol;

            return symbol?.TypeArguments.Length ?? 0;
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

        public static void Write(this ConstructorDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
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

        public static void Write(this FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IFieldSymbol)context.SemanticModel.GetDeclaredSymbol(syntax.Declaration.Variables.Single());
            
            textWriter.WriteLine("_M.IM(members, '{0}', {{", symbol.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Field',");
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void WriteDefaultValue(this FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, bool @static = false)
        {
            var isStatic = syntax.Modifiers.Any(n => n.GetKind().Equals(SyntaxKind.StaticKeyword));
            var isConst = syntax.Modifiers.Any(n => n.GetKind().Equals(SyntaxKind.ConstKeyword));

            if ((isStatic || isConst) != @static)
            {
                return;
            }

            syntax.Declaration.WriteDefaultValue(textWriter, context);
        }

        public static void WriteInitializeValue(this FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var name = syntax.Declaration.Variables.Single().Identifier.Text;
            textWriter.WriteLine($"if not(values.{name} == nil) then element[typeObject.Level].{name} = values.{name}; end");
        }

        public static void Write(this PropertyDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

            textWriter.WriteLine("_M.IM(members, '{0}',{{", symbol.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = '{0}',",
                syntax.AccessorList.IsAutoProperty() ? "AutoProperty" : "Property");
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
            textWriter.Write("returnType = ");
            context.TypeReferenceWriter.WriteTypeReference(symbol.Type, textWriter);
            textWriter.WriteLine(";");

            syntax.AccessorList.Write(textWriter, context);
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static bool IsAutoProperty(this AccessorListSyntax syntax)
        {
            return syntax.Accessors.Any(a => a.Body == null);
        }

        public static void WriteDefaultValue(this PropertyDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, bool isStaticFilter = false)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);
            if (symbol.IsStatic != isStaticFilter)
            {
                return;
            }

            textWriter.Write($"{symbol.Name} = _M.DV(");
            context.TypeReferenceWriter.WriteTypeReference(symbol.Type, textWriter);
            textWriter.WriteLine("),");
        }

        public static void WriteInitializeValue(this PropertyDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine($"if not(values.{syntax.Identifier.Text} == nil) then element[typeObject.Level].{syntax.Identifier.Text} = values.{syntax.Identifier.Text}; end");
        }

        public static void Write(this IndexerDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

            textWriter.WriteLine("_M.IM(members,'#',{");
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Indexer',");
            textWriter.WriteLine($"scope = '{symbol.DeclaredAccessibility}',");

            if (!syntax.AccessorList.IsAutoProperty())
            {
                syntax.AccessorList.Write(textWriter, context);
            }
            else
            {
                textWriter.Write("returnType = ");
                context.TypeReferenceWriter.WriteTypeReference(symbol.Type, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void Write(this EnumMemberDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var index = ((EnumDeclarationSyntax) syntax.Parent).Members.IndexOf(syntax);
            if (index == 0)
            {
                textWriter.WriteLine($"__default = \"{syntax.Identifier.Text}\",");
            }

            textWriter.Write($"[\"{syntax.Identifier.Text}\"] = \"{syntax.Identifier.Text}\"");
        }

        public static void Write(this EnumDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);
            textWriter.WriteLine("[0] = _M.EN({");
            textWriter.Indent++;

            syntax.Members.Write(Write, textWriter, context, () => textWriter.WriteLine(","));

            textWriter.Indent--;
            textWriter.WriteLine("");

            var namespaceName = context.SemanticAdaptor.GetFullNamespace(symbol);
            var name = context.SemanticAdaptor.GetName(symbol);

            textWriter.Write($"}},'{name}','{namespaceName}',");
            context.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine("),");
        }

        public static void Write(this NamespaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var state = context.PartialElementState;
            var isFirstNamespace = state.IsFirst;
            var isLastNamespace = state.IsLast;

            var members = syntax.Members.Where(member => member.GetNumGenerics(context) == state.NumberOfGenerics).ToArray();

            for (var index = 0; index < members.Length; index++)
            {
                state.IsFirst = isFirstNamespace && index == 0;
                state.IsLast = isLastNamespace && index == members.Length - 1;
                members[index].Write(textWriter, context);
            }
        }
    }
}