namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class InterfaceExtensions
    {
        public static void Write(this InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            WrappingException.TryActionAndWrapException(() =>
            {
                switch ((InterfaceState) (context.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        WriteOpen(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int) InterfaceState.Members;
                        break;
                    case InterfaceState.Members:
                        WriteMembers(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int) InterfaceState.Close;
                        break;
                    case InterfaceState.Close:
                        WriteClose(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                }
            },
                $"In visiting of interface {syntax.Identifier.Text}. State: {((InterfaceState) (context.PartialElementState.CurrentState ?? 0))}");
        }

        private static void WriteOpen(InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (!context.PartialElementState.IsFirst) return;

            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);
            var adaptor = context.SemanticAdaptor;

            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", adaptor.GetGenerics(symbol).Length);
            textWriter.Indent++;

            WriteGenericsMapping(syntax, textWriter, context);

            textWriter.Write(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Interface',",
                adaptor.GetName(symbol), adaptor.GetFullNamespace(symbol), adaptor.GetGenerics(symbol).Length);
            context.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine(");");

            WriteImplements(syntax, textWriter, context, symbol);
            WriteAttributes(syntax, textWriter, context);
        }

        private static void WriteClose(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (!context.PartialElementState.IsLast) return;

            textWriter.WriteLine("return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");
        }

        private static void WriteGenericsMapping(InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local genericsMapping = {");
            syntax.TypeParameterList?.Write(textWriter, context);
            textWriter.WriteLine("};");
        }

        private static void WriteImplements(InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, INamedTypeSymbol symbol)
        {
            textWriter.WriteLine("local implements = {");
            textWriter.Indent++;

            foreach (var interfaceType in symbol.Interfaces)
            {
                context.TypeReferenceWriter.WriteTypeReference(interfaceType, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private static void WriteAttributes(InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            syntax.AttributeLists.Write(SyntaxNodeExtensions.Write, textWriter, context);
        }

        private static void WriteMembers(InterfaceDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = {};");
                textWriter.WriteLine("_M.GAM(members, implements);");
            }

            syntax.Members.Write(MemberExtensions.Write, textWriter, context);

            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return members;");
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }
    }
}