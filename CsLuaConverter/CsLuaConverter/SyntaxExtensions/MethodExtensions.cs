
namespace CsLuaConverter.SyntaxExtensions
{
    using System.Linq;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class MethodExtensions
    {
        public static void Write(this MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

            WriteMethodGenericsMapping(syntax, textWriter, context);
            WriteMethodMember(syntax, textWriter, context, symbol);
        }

        private static void WriteMethodMember(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            textWriter.WriteLine("_M.IM(members, '{0}', {{", symbol.Name);
            textWriter.Indent++;

            WriteLevel(textWriter);
            WriteMemberType(textWriter);
            WriteScope(textWriter, symbol);
            WriteIsStatic(textWriter, symbol);
            WriteNumOfMethodGenerics(textWriter, symbol);
            WriteSignatureHash(textWriter, context, symbol);
            WriteOverride(textWriter, symbol);
            WriteIsParams(textWriter, symbol);
            WriteReturnType(textWriter, context, symbol);
            WriteGenerics(syntax, textWriter);
            WriteBodyFunc(syntax, textWriter, context, symbol);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        private static void WriteBodyFunc(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            if (syntax.Body == null)
            {
                return;
            }

            textWriter.Write("func = function(element");

            if (syntax.TypeParameterList != null)
            {
                textWriter.Write(", methodGenericsMapping, methodGenerics");
            }

            syntax.ParameterList.Write(textWriter, context);

            textWriter.WriteLine(")");

            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.Indent++;
                WriteParamVariableInit(textWriter, context, symbol);
                textWriter.Indent--;
            }

            syntax.Body.Write(textWriter, context);
            textWriter.WriteLine("end");
        }

        private static void WriteGenerics(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter)
        {
            if (syntax.TypeParameterList != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }
        }

        private static void WriteReturnType(IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            if (symbol.ReturnsVoid)
            {
                return;
            }

            textWriter.Write("returnType = function() return ");
            context.TypeReferenceWriter.WriteTypeReference(symbol.ReturnType, textWriter);
            textWriter.WriteLine(" end,");
        }

        private static void WriteIsParams(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }
        }

        private static void WriteOverride(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            if (symbol.IsOverride)
            {
                textWriter.WriteLine("override = true,");
            }
        }

        private static void WriteSignatureHash(
            IIndentedTextWriterWrapper textWriter,
            IContext context,
            IMethodSymbol symbol)
        {
            textWriter.Write("signatureHash = ");
            context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            textWriter.WriteLine(",");
        }

        private static void WriteNumOfMethodGenerics(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("numMethodGenerics = {0},", symbol.TypeArguments.Length);
        }

        private static void WriteIsStatic(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
        }

        private static void WriteScope(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);
        }

        private static void WriteMemberType(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("memberType = 'Method',");
        }

        private static void WriteLevel(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("level = typeObject.Level,");
        }

        private static void WriteMethodGenericsMapping(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.TypeParameterList == null)
            {
                return;
            }

            textWriter.Write("local methodGenericsMapping = {");
            syntax.TypeParameterList.Write(textWriter, context);
            textWriter.WriteLine("};");
            textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");
        }

        private static void WriteParamVariableInit(IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            var parameter = symbol.Parameters.Last();
            textWriter.Write("local ");
            textWriter.Write(parameter.Name);
            textWriter.Write(" = (");
            context.TypeReferenceWriter.WriteInteractionElementReference(parameter.Type, textWriter);
            textWriter.WriteLine("._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});");
        }

        public static void Write(this TypeParameterSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(syntax.Identifier.Text);
        }

        public static void Write(this TypeParameterListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var c = 1;
            foreach (var visitor in syntax.Parameters)
            {
                if (c > 1)
                {
                    textWriter.Write(",");
                }

                textWriter.Write("['");
                visitor.Write(textWriter, context);
                textWriter.Write("'] = {0}", c);
                c++;
            }
        }
    }
}