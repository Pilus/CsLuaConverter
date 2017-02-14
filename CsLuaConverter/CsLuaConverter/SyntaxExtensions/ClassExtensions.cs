namespace CsLuaConverter.SyntaxExtensions
{
    using System.Linq;
    using CsLuaConverter.CodeTreeLuaVisitor.Member;
    using CsLuaConverter.Context;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
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

        public static void WriteOpen(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                var symbol = context.CurrentClass;

                textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", context.SemanticAdaptor.HasTypeGenerics(symbol) ? context.SemanticAdaptor.GetGenerics(symbol).Length : 0);
                textWriter.Indent++;

                syntax.WriteGenericsMapping(textWriter, context);
                ClassExtensions.WriteTypeGeneration(symbol, textWriter, context);
                ClassExtensions.WriteBaseInheritance(symbol, textWriter, context);
                ClassExtensions.WriteTypePopulation(symbol, textWriter, context);
            }
        }

        public static void WriteStaticValues(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("staticValues[typeObject.Level] = {");
                textWriter.Indent++;
            }

            foreach (var property in syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                PropertyDeclarationVisitor.WriteDefaultValue(property, textWriter, context, true);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                FieldDeclarationVisitor.WriteDefaultValue(field, textWriter, context, true);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("};");
            }
        }

        public static void WriteInitialize(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {

            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local initialize = function(element, values)");
                textWriter.Indent++;

                textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");
            }

            foreach (var property in syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                PropertyDeclarationVisitor.WriteInitializeValue(property, textWriter, context);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                FieldDeclarationVisitor.WriteInitializeValue(field, textWriter, context);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        public static void WriteClose(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;");
                textWriter.Indent--;
                textWriter.WriteLine("end,");
                context.CurrentClass = null;
            }
        }

        public static void WriteFooter(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.CurrentClass;

            if (!context.PartialElementState.IsFirst || HasCsLuaAddOnAttribute(symbol) != true)
            {
                return;
            }
            
            textWriter.Write("(");
            textWriter.Write(context.SemanticAdaptor.GetFullName(symbol));
            textWriter.WriteLine("._C_0_0() % _M.DOT).Execute();");
        }

        private static bool HasCsLuaAddOnAttribute(INamedTypeSymbol symbol)
        {
            var attributes = symbol.GetAttributes();
            return attributes!= null && attributes.Any(attribute => attribute.AttributeClass.Name == nameof(CsLuaAddOnAttribute));
        }
    }
}