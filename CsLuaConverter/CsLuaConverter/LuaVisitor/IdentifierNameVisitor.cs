namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class IdentifierNameVisitor : IVisitor<IdentifierName>
    {
        public void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            Visit(element, textWriter, providers, false);
        }


        public static void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, bool writeAsIs)
        {
            WriteOpen(element, textWriter, providers, writeAsIs);
            WriteClose(element, textWriter, providers, writeAsIs);
        }

        public static void WriteOpen(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, bool writeAsIs)
        {
            var type = DetermineType(element, providers, writeAsIs);

            if (element.InnerElement != null)
            {
                if (element.InnerElement is SimpleMemberAccessExpression)
                {
                    SimpleMemberAccessExpressionVisitor.WriteOpen((SimpleMemberAccessExpression) element.InnerElement, textWriter, providers);
                }
                else if (element.InnerElement is BracketedArgumentList)
                {
                    BracketedArgumentListVisitor.WriteOpen((BracketedArgumentList)element.InnerElement, textWriter, providers);
                }
                else
                {
                    throw new LuaVisitorException("Unhandled idenfier name inner element");
                }
            }

            switch (type)
            {
                case IdentifyerType.AsIs:
                    WriteAsIsOpen(element, textWriter, providers);
                    break;
                case IdentifyerType.AsRef:
                case IdentifyerType.AsVar:
                case IdentifyerType.AsGeneric:
                    break;
            }
        }

        public static void WriteClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, bool writeAsIs)
        {
            var type = DetermineType(element, providers, writeAsIs);

            switch (type)
            {
                case IdentifyerType.AsVar:
                    WriteLocalVar(element, textWriter, providers);
                    break;
                case IdentifyerType.AsIs:
                    WriteAsIsClose(element, textWriter, providers);
                    break;
                case IdentifyerType.AsGeneric:
                    WriteAsGenericReference(element, textWriter, providers);
                    break;
                case IdentifyerType.AsRef:
                    WriteAsReferenceClose(element, textWriter, providers);
                    break;
            }

            if (element.InnerElement != null)
            {
                if (element.InnerElement is SimpleMemberAccessExpression)
                {
                    SimpleMemberAccessExpressionVisitor.WriteClose((SimpleMemberAccessExpression)element.InnerElement, textWriter, providers);
                }
                else if (element.InnerElement is BracketedArgumentList)
                {
                    BracketedArgumentListVisitor.WriteClose((BracketedArgumentList)element.InnerElement, textWriter, providers);
                }
                else
                {
                    throw new LuaVisitorException("Unhandled idenfier name inner element");
                }
            }
        }


        private static IdentifyerType DetermineType(IdentifierName element, IProviders providers, bool writeAsIs)
        {
            if (writeAsIs)
            {
                return IdentifyerType.AsIs;
            }

            if (element.Names.FirstOrDefault() == "var")
            {
                return IdentifyerType.AsVar;
            }

            if (providers.GenericsRegistry.IsGeneric(element.Names.SingleOrDefault()))
            {
                return IdentifyerType.AsGeneric;
            }

            return IdentifyerType.AsRef;
        }


        private static void WriteAsIsOpen(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(new string('(', element.Names.Count - 1));
        }

        private static void WriteAsIsClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            for (var i = 0; i < element.Names.Count; i++)
            {
                textWriter.Write(element.Names[i]);

                if (i < element.Names.Count - 1)
                {
                    textWriter.Write(" % _M.DOT).");
                }
            }
        }

        private static void WriteAsReferenceClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scopeElement = providers.NameProvider.GetScopeElement(element.Names.First());
            if (scopeElement != null)
            {
                if (scopeElement.ClassPrefix != null)
                {
                    if (scopeElement.ClassPrefix == "element")
                    {
                        textWriter.Write("(element%_M.DOT_LVL(typeObject.Level)).");
                    }
                    else
                    {
                        textWriter.Write("({0} % _M.DOT).", scopeElement.ClassPrefix);
                    }
                }
                textWriter.Write(scopeElement.Name);

                if (element.Names.Count > 1)
                {
                    throw new LuaVisitorException("Scope element with further reference not supported.");
                }
            }
            else
            {
                textWriter.Write(providers.TypeProvider.LookupType(element.Names).ToString());
            }
        }

        private static void WriteAsGenericReference(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("generics[genericsMapping['{0}']]", element.Names.Single());
        }

        private static void WriteLocalVar(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local ");
        }
    }

    public enum IdentifyerType
    {
        AsVar,
        AsIs,
        AsRef,
        AsGeneric,
    }
}