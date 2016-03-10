namespace CsLuaConverter.CodeElementAnalysis.Helpers
{
    using System;
    using System.Linq;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Statements;

    public static class TypeKnowledgeHelper
    {
        public static TypeKnowledge GetTypeKnowledge(BaseElement element, IProviders providers, TypeKnowledge onType = null)
        {
            switch (element.GetType().Name)
            {
                case nameof(IdentifierName):
                    return GetTypeKnowledge((IdentifierName)element, providers, onType);
                case nameof(PredefinedType):
                    return GetTypeKnowledge((PredefinedType)element, providers, onType);
                case nameof(Statement):
                    return GetTypeKnowledge((Statement)element, providers, onType);
                case nameof(ThisExpression):
                    return GetTypeKnowledge((ThisExpression)element, providers, onType);
                case nameof(SimpleMemberAccessExpression):
                    return GetTypeKnowledge((SimpleMemberAccessExpression)element, providers, onType);
                case nameof(BracketedArgumentList):
                    return GetTypeKnowledge((BracketedArgumentList)element, providers, onType);
                default:
                    throw new Exception("Can not find type knowledge from type: " + element.GetType().Name);
            }
        }

        private static TypeKnowledge GetTypeKnowledge(IdentifierName element, IProviders providers, TypeKnowledge onType)
        {
            TypeKnowledge typeKnowledge = null;
            string additionalString = null;

            if (onType != null)
            {
                typeKnowledge = onType;
                additionalString = string.Join(".", element.Names);
            }
            else
            {
                var variableResult = providers.NameProvider.GetScopeElement(element.Names.First());
                if (variableResult != null)
                {
                    typeKnowledge = variableResult.Type;
                    additionalString = string.Join(".", element.Names.Skip(1));
                }
                else
                {
                    var typeResult = providers.TypeProvider.LookupType(element.Names);
                    typeKnowledge = new TypeKnowledge(typeResult.TypeObject);
                    additionalString = typeResult.AdditionalString;

                }
            }

            if (!string.IsNullOrEmpty(additionalString))
            {
                typeKnowledge = typeKnowledge.GetTypeKnowledgeForSubElement(additionalString);
            }

            if (element.InnerElement == null)
            {
                return typeKnowledge;
            }

            return GetTypeKnowledge(element.InnerElement, providers, typeKnowledge);
        }

        private static TypeKnowledge GetTypeKnowledge(PredefinedType element, IProviders providers, TypeKnowledge onType)
        {
            var type = providers.TypeProvider.LookupType(element.Text).TypeObject;
            return new TypeKnowledge(element.IsArray ? type.MakeArrayType() : type);
        }

        private static TypeKnowledge GetTypeKnowledge(Statement element, IProviders providers, TypeKnowledge onType)
        {
            return GetTypeKnowledge(element.ContainedElements.Single(), providers);
        }

        private static TypeKnowledge GetTypeKnowledge(ThisExpression element, IProviders providers, TypeKnowledge onType)
        {
            var typeKnowledge = providers.NameProvider.GetScopeElement("this").Type;

            if (element.InnerElement == null)
            {
                return typeKnowledge;
            }

            return GetTypeKnowledge(element.InnerElement, providers, typeKnowledge);
        }
        
        private static TypeKnowledge GetTypeKnowledge(SimpleMemberAccessExpression element, IProviders providers, TypeKnowledge onType)
        {
            if (onType == null)
            {
                throw new Exception("Expected target type.");
            }

            return GetTypeKnowledge(element.InnerElement, providers, onType);
        }

        private static TypeKnowledge GetTypeKnowledge(BracketedArgumentList element, IProviders providers, TypeKnowledge onType)
        {
            var typeKnowledge = onType.GetIndexerValueType();

            if (element.InnerElement == null)
            {
                return typeKnowledge;
            }

            return GetTypeKnowledge(element.InnerElement, providers, typeKnowledge);
        }
    }
}