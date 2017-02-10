namespace CsLuaConverter.SyntaxExtensions
{
    using System.Linq;
    using CsLuaConverter.CodeTreeLuaVisitor.Member;
    using CsLuaConverter.Context;
    using CsLuaFramework;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ClassExtensions
    {
        public static void WriteGenericsMapping(this ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local genericsMapping = {");
            syntax.TypeParameterList?.Write(textWriter, context);
            textWriter.WriteLine("};");
        }

        public static void WriteTypeGeneration(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var adaptor = context.SemanticAdaptor;
            textWriter.Write(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Class', ",
                adaptor.GetName(symbol),
                adaptor.GetFullNamespace(symbol),
                adaptor.GetGenerics(symbol).Length);
            context.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine(");");
        }

        public static void WriteBaseInheritance(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = ");

            context.TypeReferenceWriter.WriteInteractionElementReference(symbol.BaseType, textWriter);

            textWriter.WriteLine(".__meta(staticValues);");
        }

        public static void WriteTypePopulation(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            foreach (var interfaceSymbol in symbol.Interfaces)
            {
                if (context.SemanticAdaptor.IsInterface(interfaceSymbol)
                    && context.SemanticAdaptor.GetName(interfaceSymbol) != nameof(ICsLuaAddOn))
                {
                    textWriter.Write("table.insert(implements, ");
                    context.TypeReferenceWriter.WriteTypeReference(interfaceSymbol, textWriter);
                    textWriter.WriteLine(");");
                }
            }

            textWriter.WriteLine("typeObject.baseType = baseTypeObject;");
            textWriter.WriteLine("typeObject.level = baseTypeObject.level + 1;");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        public static void WriteTypeGenerator(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local elementGenerator = function()");
                textWriter.Indent++;

                textWriter.WriteLine("local element = baseElementGenerator();");
                textWriter.WriteLine("element.type = typeObject;");
                textWriter.WriteLine("element[typeObject.Level] = {");

                textWriter.Indent++;
            }

            foreach (var property in syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                PropertyDeclarationVisitor.WriteDefaultValue(property, textWriter, context);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                FieldDeclarationVisitor.WriteDefaultValue(field, textWriter, context);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;

                textWriter.WriteLine("};");

                textWriter.WriteLine("return element;");
                textWriter.Indent--;

                textWriter.WriteLine("end");
            }
        }
    }
}