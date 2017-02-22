namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ClassExtensions
    {
        private static Dictionary<ClassDeclarationSyntax, INamedTypeSymbol> symbolCache = new Dictionary<ClassDeclarationSyntax, INamedTypeSymbol>();
        public static void Write(this ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = symbolCache.ContainsKey(syntax) ? symbolCache[syntax] : context.SemanticModel.GetDeclaredSymbol(syntax);
            symbolCache[syntax] = symbol;

            WrappingException.TryActionAndWrapException(() =>
            {
                switch ((ClassState)(context.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        WriteOpen(syntax, textWriter, context, symbol);
                        context.PartialElementState.NextState = (int)ClassState.TypeGeneration;
                        break;
                    case ClassState.TypeGeneration:
                        WriteTypeGenerator(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteStaticValues;
                        break;
                    case ClassState.WriteStaticValues:
                        WriteStaticValues(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteInitialize;
                        break;
                    case ClassState.WriteInitialize:
                        WriteInitialize(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.WriteMembers;
                        break;
                    case ClassState.WriteMembers:
                        WriteMembers(syntax, textWriter, context);
                        context.PartialElementState.NextState = (int)ClassState.Close;
                        break;
                    case ClassState.Close:
                        WriteClose(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                    case ClassState.Footer:
                        WriteFooter(textWriter, context, symbol);
                        context.PartialElementState.NextState = null;
                        break;
                }
            },
                $"In visiting of class {context.SemanticAdaptor.GetFullName(symbol)}. State: {((ClassState)(context.PartialElementState.CurrentState ?? 0))}");
        }

        private static void WriteGenericsMapping(this ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local genericsMapping = {");
            syntax.TypeParameterList?.Write(textWriter, context);
            textWriter.WriteLine("};");
        }

        private static void WriteTypeGeneration(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
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

        private static void WriteBaseInheritance(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = ");

            context.TypeReferenceWriter.WriteInteractionElementReference(symbol.BaseType, textWriter);

            textWriter.WriteLine(".__meta(staticValues);");
        }

        private static void WriteTypePopulation(ITypeSymbol symbol, IIndentedTextWriterWrapper textWriter, IContext context)
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

        private static void WriteTypeGenerator(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
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
                property.WriteDefaultValue(textWriter, context);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                field.WriteDefaultValue(textWriter, context);
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

        private static void WriteOpen(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, INamedTypeSymbol symbol)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", context.SemanticAdaptor.HasTypeGenerics(symbol) ? context.SemanticAdaptor.GetGenerics(symbol).Length : 0);
                textWriter.Indent++;

                syntax.WriteGenericsMapping(textWriter, context);
                WriteTypeGeneration(symbol, textWriter, context);
                WriteBaseInheritance(symbol, textWriter, context);
                WriteTypePopulation(symbol, textWriter, context);
            }
        }

        private static void WriteStaticValues(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("staticValues[typeObject.Level] = {");
                textWriter.Indent++;
            }

            foreach (var property in syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                property.WriteDefaultValue(textWriter, context, true);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                field.WriteDefaultValue(textWriter, context, true);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("};");
            }
        }

        private static void WriteInitialize(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {

            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local initialize = function(element, values)");
                textWriter.Indent++;

                textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");
            }

            foreach (var property in syntax.Members.OfType<PropertyDeclarationSyntax>())
            {
                property.WriteInitializeValue(textWriter, context);
            }

            foreach (var field in syntax.Members.OfType<FieldDeclarationSyntax>())
            {
                field.WriteInitializeValue(textWriter, context);
            }

            if (context.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }

        private static void WriteClose(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;");
                textWriter.Indent--;
                textWriter.WriteLine("end,");
            }
        }

        private static void WriteFooter(IIndentedTextWriterWrapper textWriter, IContext context, INamedTypeSymbol symbol)
        {
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
            return attributes!= null && ImmutableArrayExtensions.Any(attributes, attribute => attribute.AttributeClass.Name == nameof(CsLuaAddOnAttribute));
        }

        private static void WriteMembers(ClassDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = _M.RTEF(getBaseMembers);");
            }

            if (context.PartialElementState.DefinedConstructorWritten == false && syntax.Members.OfType<ConstructorDeclarationSyntax>().Any())
            {
                context.PartialElementState.DefinedConstructorWritten = true;
            }

            if (context.PartialElementState.DefinedConstructorWritten == false && context.PartialElementState.IsLast)
            {
                // TODO: This might cause issues in partial classes where the constructors are placed in the first part.
                MemberExtensions.WriteEmptyConstructor(textWriter);
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