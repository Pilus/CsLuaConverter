namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class IdentifierNameVisitor : IOpenCloseVisitor<IdentifierName>
    {
        public void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            Visit(element, textWriter, providers, null);
        }

        public void WriteOpen(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteOpen(element, textWriter, providers, IdentifyerType.AsIs);
        }

        public void WriteClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            WriteClose(element, textWriter, providers, IdentifyerType.AsIs);
        }

        public static void Visit(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, IdentifyerType? type)
        {
            WriteOpen(element, textWriter, providers, type);
            WriteClose(element, textWriter, providers, type);
        }

        private static void WriteOpen(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, IdentifyerType? type)
        {
            if (element.InnerElement != null)
            {
                VisitorList.WriteOpen(element.InnerElement);
                textWriter.Write("(");
            }
            

            var identifierType = type ?? DetermineType(element, providers);

            switch (identifierType)
            {
                case IdentifyerType.AsIs:
                    WriteAsIsOpen(element, textWriter, providers);
                    break;
                case IdentifyerType.AsRef:
                case IdentifyerType.AsVar:
                case IdentifyerType.AsGeneric:
                case IdentifyerType.AsScope:
                    break;
            }
        }

        private static void WriteClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers, IdentifyerType? type)
        {
            var identifierType = type ?? DetermineType(element, providers);

            switch (identifierType)
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
                case IdentifyerType.AsScope:
                    WriteAsScopeClose(element, textWriter, providers);
                    break;
            }

            if (element.InnerElement != null)
            {
                textWriter.Write(" % _M.DOT)");
                VisitorList.WriteClose(element.InnerElement);
            }
            
        }


        private static IdentifyerType DetermineType(IdentifierName element, IProviders providers)
        {
            if (element.Names.FirstOrDefault() == "var")
            {
                return IdentifyerType.AsVar;
            }

            if (providers.GenericsRegistry.IsGeneric(element.Names.SingleOrDefault()))
            {
                return IdentifyerType.AsGeneric;
            }

            if (providers.NameProvider.GetScopeElement(element.Names.First()) != null)
            {
                return IdentifyerType.AsScope;
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

        private static void WriteAsScopeClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scopeElement = providers.NameProvider.GetScopeElement(element.Names.First());
            if (scopeElement.ClassPrefix != null)
            {
                //if (scopeElement.ClassPrefix == "element")
                //{
                //    textWriter.Write("(element%_M.DOT_LVL(typeObject.Level)).");
                //}
                //else
                //{
                    textWriter.Write("({0} % _M.DOT).", scopeElement.ClassPrefix);
                //}
            }
            textWriter.Write(scopeElement.Name);

            if (element.Names.Count > 1)
            {
                throw new LuaVisitorException("Scope element with further reference not supported.");
            }
        }


        private static void WriteAsReferenceClose(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(providers.TypeProvider.LookupType(element.Names).ToString());
        }

        private static void WriteAsGenericReference(IdentifierName element, IndentedTextWriter textWriter, IProviders providers)
        {
            var name = element.Names.Single();
            var scope = providers.GenericsRegistry.GetGenericScope(name);
            if (scope.Equals(GenericScope.Class))
            {
                textWriter.Write("generics[genericsMapping['{0}']]", name);
            }
            else
            {
                textWriter.Write("methodGenerics[methodGenericsMapping['{0}']]", name);
            }
            
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
        AsScope,
    }
}