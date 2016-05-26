namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;

    public static class TypeKnowledgeExtensions
    {
        public static void WriteAsType(this TypeKnowledge typeKnowledge, IIndentedTextWriterWrapper textWriter,
            IProviders providers)
        {
            if (typeKnowledge.GetTypeObject().IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            typeKnowledge.WriteAsReference(textWriter, providers);
            textWriter.Write(".__typeof");
        }


        public static TypeKnowledge ApplyMissingGenerics(this TypeKnowledge typeKnowledge, TypeKnowledge[] generics)
        {
            var type = typeKnowledge.GetTypeObject();

            if (type.IsGenericParameter)
            {
                throw new NotImplementedException();
            }

            if (!type.IsGenericType) return typeKnowledge;

            var list = new List<System.Type>();
            var genericArgs = type.GetGenericArguments();
            for (var index = 0; index < genericArgs.Length; index++)
            {
                var genericArgument = genericArgs[index];
                list.Add( ApplyMissingGenerics(genericArgument, generics[index]?.GetTypeObject()));
            }

            if (list.All(v => v == null))
            {
                return typeKnowledge;
            }

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index] == null)
                {
                    list[index] = genericArgs[index];
                }
            }

            var genericType = type.GetGenericTypeDefinition();
            return new TypeKnowledge(genericType.MakeGenericType(list.ToArray()));
        }

        private static System.Type ApplyMissingGenerics(System.Type type, System.Type genericType)
        {
            if (type.IsGenericParameter)
            {
                TypeKnowledge.Providers.GenericsRegistry.SetGenerics(type.Name, GenericScope.Invocation, type, genericType);
                return genericType;
            }

            if (!type.IsGenericType || genericType == null)
            {
                return null;
            }

            var list = new List<System.Type>();
            var genericArgs = type.GetGenericArguments();
            var subGenericTypes = genericType.GetGenericArguments();

            for (var index = 0; index < genericArgs.Length; index++)
            {
                var genericArgument = genericArgs[index];
                list.Add(ApplyMissingGenerics(genericArgument, subGenericTypes[index]));
            }

            if (list.All(v => v == null))
            {
                return type;
            }

            for (int index = 0; index < list.Count; index++)
            {
                if (list[index] == null)
                {
                    list[index] = genericArgs[index];
                }
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef.MakeGenericType(list.ToArray());
        }


        public static TypeKnowledge ApplyMethodGenerics(this TypeKnowledge typeKnowledge, TypeKnowledge[] types)
        {
            if (typeKnowledge.MethodGenerics == null)
            {
                return typeKnowledge;
            }

            var mapping = new Dictionary<string, System.Type>();

            for (int index = 0; index < typeKnowledge.MethodGenerics.Length; index++)
            {
                var methodGeneric = typeKnowledge.MethodGenerics[index];
                var type = types[index];
                mapping[methodGeneric.Name] = type.GetTypeObject();
            }

            return new TypeKnowledge(ApplyMethodGenericsType(typeKnowledge.GetTypeObject(), mapping));
        }

        private static System.Type ApplyMethodGenericsType(System.Type type, Dictionary<string, System.Type> genericsMapping)
        {
            if (type.IsGenericParameter)
            {
                return genericsMapping[type.Name];
            }

            if (!type.IsGenericType) return type;

            var appliedGenerics = type.GetGenericArguments().Select(t => ApplyMethodGenericsType(t, genericsMapping)).ToArray();

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef.MakeGenericType(appliedGenerics);
        }

        
        public static TypeKnowledge ResolveGenerics(this TypeKnowledge typeKnowledge, IProviders providers)
        {
            var type = typeKnowledge.GetTypeObject();
            var newType = ResolveGenerics(type, providers);
            return newType == null ? typeKnowledge : new TypeKnowledge(newType);
        }

        private static System.Type ResolveGenerics(System.Type type, IProviders providers)
        {
            if (type.IsGenericParameter)
            {
                var resolvedType = providers.GenericsRegistry.GetType(type.Name);
                if (resolvedType == null)
                {
                    throw new Exception($"Could not resolve generic {type.Name}.");
                }

                return resolvedType;
            }

            if (!type.IsGenericType) return null;

            var genericArgs = type.GetGenericArguments();

            var list = genericArgs.Select(genericArgument => ResolveGenerics(genericArgument, providers)).ToList();

            if (list.All(v => v == null))
            {
                return null;
            }

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index] == null)
                {
                    list[index] = genericArgs[index];
                }
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef.MakeGenericType(list.ToArray());
        }

        public static void WriteAsReference(this TypeKnowledge typeKnowledge, IIndentedTextWriterWrapper textWriter,
            IProviders providers)
        {
            if (typeKnowledge.GetTypeObject().IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            textWriter.Write(typeKnowledge.GetFullName());

            var generics = typeKnowledge.GetGenerics();
            if (!generics.Any())
            {
                return;
            }

            textWriter.Write("[{");
            foreach (var generic in generics)
            {
                generic.WriteAsType(textWriter, providers);
                textWriter.Write(", ");
            }
            textWriter.Write("}]");
        }

        public static TypeKnowledge[] GetInputArgs(this TypeKnowledge typeKnowledge)
        {
            if (typeKnowledge.GetTypeObject().IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            var type = typeKnowledge.GetTypeObject();
            var del = GetDelegate(type);
            if (del == null)
            {
                throw new VisitorException($"Cannot get invocation input args on current type {typeKnowledge.GetFullName()}.");
            }

            return del.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType.IsGenericParameter ? null : new TypeKnowledge(p.ParameterType)).ToArray();
        }

        public static bool IsDelegate(this TypeKnowledge typeKnowledge)
        {
            return GetDelegate(typeKnowledge.GetTypeObject()) != null;
        }

        private static System.Type GetDelegate(System.Type type)
        {
            if (type.BaseType == typeof (MulticastDelegate))
            {
                return type;
            }

            return type.BaseType?.BaseType != null ? GetDelegate(type.BaseType) : null;
        }

        public static TypeKnowledge GetReturnArg(this TypeKnowledge typeKnowledge)
        {
            if (typeKnowledge.GetTypeObject().IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            var type = typeKnowledge.GetTypeObject();
            var del = GetDelegate(type);
            if (del == null)
            {
                throw new VisitorException($"Cannot get invocation input args on current type {typeKnowledge.GetFullName()}.");
            }

            return new TypeKnowledge(del.GetMethod("Invoke").ReturnType);
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(this TypeKnowledge typeKnowledge, TypeKnowledge otherTypeKnowledge)
        {
            var type = typeKnowledge.GetTypeObject();

            if (typeKnowledge.GetTypeObject().IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            var otherType = otherTypeKnowledge?.GetTypeObject();

            return otherType == null ? 0 : ScoreForHowWellOtherTypeFitsThis(type, otherType);
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(System.Type type, System.Type otherType)
        {
            if (type.IsGenericParameter)
            {
                throw new Exception("Cannot perform action on a generic type.");
            }

            var c = 0;

            while (!type.IsAssignableFrom(otherType))
            {
                otherType = otherType.BaseType;

                if (otherType == null)
                {
                    return null;
                }

                c++;
            }

            return type == otherType ? c : c + 1;
        }

        public static void RegisterMethodGenericsWithAppliedTypes(this TypeKnowledge delegateTypeKnowledge, TypeKnowledge[] appliedTypeKnowledges)
        {
            var delegateType = delegateTypeKnowledge.GetTypeObject();
            var genericArgs = delegateType.GetGenericArguments();
            for (int index = 0; index < genericArgs.Length; index++)
            {
                var genericArgument = genericArgs[index];
                var appliedType = appliedTypeKnowledges.Length > index
                    ? appliedTypeKnowledges[index]?.GetTypeObject()
                    : null;
                RegisterMethodGenericsWithAppliedTypes(genericArgument, appliedType);
            }
        }

        private static void RegisterMethodGenericsWithAppliedTypes(System.Type type, System.Type appliedType)
        {
            if (appliedType == null || (type.IsGenericParameter == false && type.IsGenericType == false))
            {
                return;
            }

            if (type.IsGenericParameter)
            {
                if (!TypeKnowledge.Providers.GenericsRegistry.IsGeneric(type.Name))
                {
                    TypeKnowledge.Providers.GenericsRegistry.SetGenerics(type.Name, GenericScope.Invocation, type, appliedType);
                }
                return;
            }

            var genericArgs = type.GetGenericArguments();
            var appliedGenericArgs = GetImplementationOfType(appliedType, type).GetGenericArguments();

            for (var index = 0; index < genericArgs.Length; index++)
            {
                var genericArg = genericArgs[index];
                var appliedGenericArg = appliedGenericArgs[index];
                RegisterMethodGenericsWithAppliedTypes(genericArg, appliedGenericArg);
            }
        }

        private static System.Type GetImplementationOfType(System.Type rawType, System.Type desiredType)
        {
            while (rawType != null)
            {
                if (rawType.Name == desiredType.Name && rawType.Namespace == desiredType.Namespace)
                {
                    return rawType;
                }

                foreach (var implementedInterface in rawType.GetInterfaces())
                {
                    var t = GetImplementationOfType(implementedInterface, desiredType);
                    if (t != null)
                    {
                        return t;
                    }
                }

                rawType = rawType.BaseType;
            }

            return null;
        }

        public static TypeKnowledge[] ApplyImplicitAndGenericTypes(this TypeKnowledge[] typeKnowledges, TypeKnowledge[] actualTypeKnowledges)
        {
            var list = new List<TypeKnowledge>();
            for (int index = 0; index < typeKnowledges.Length; index++)
            {
                var type = typeKnowledges[index]?.GetTypeObject();
                var actualType = actualTypeKnowledges[index]?.GetTypeObject();

                if (type == null)
                {
                    list.Add(new TypeKnowledge(typeof(object)));
                    continue;
                }
                
                var modifiedType = ApplyImplicitAndGenericTypes(type, actualType);

                if (modifiedType != null)
                {
                    list.Add(new TypeKnowledge(modifiedType));
                }
                else
                {
                    list.Add(typeKnowledges[index]);
                }
            }

            return list.ToArray();
        }

        private static System.Type ApplyImplicitAndGenericTypes(System.Type type, System.Type actualType)
        {
            if (type.IsGenericParameter)
            {
                var reg = TypeKnowledge.Providers.GenericsRegistry;
                if (reg.IsGeneric(type.Name))
                {
                    return reg.GetType(type.Name);
                }

                reg.SetGenerics(type.Name, GenericScope.Invocation, type, actualType);
                return actualType;
            }

            if (type.Name != actualType?.Name || type.Namespace != actualType.Namespace)
            {
                // Stop looking at implicit values.
                actualType = null;
            }

            if (!type.IsGenericType || (actualType != null && !actualType.IsGenericType)) return null;

            var list = new List<System.Type>();
            var genericArgs = type.GetGenericArguments();
            var subGenericTypes = actualType?.GetGenericArguments();

            for (var index = 0; index < genericArgs.Length; index++)
            {
                var genericArgument = genericArgs[index];
                list.Add(ApplyImplicitAndGenericTypes(genericArgument, subGenericTypes?[index]));
            }

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index] == null)
                {
                    list[index] = genericArgs[index];
                }
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef.MakeGenericType(list.ToArray());
        }

        public static int? ScoreForHowWellOtherTypeFitsThis(this TypeKnowledge[] typeKnowledges, TypeKnowledge[] otherTypeKnowledges, bool isParams = false)
        {
            var c = 0;
            for (int index = 0; index < otherTypeKnowledges.Length; index++)
            {
                var otherTypeKnowledge = otherTypeKnowledges[index];

                TypeKnowledge typeKnowledge;
                if (isParams && index >= typeKnowledges.Length - 1)
                {
                    typeKnowledge = typeKnowledges[typeKnowledges.Length - 1].GetArrayGeneric();
                }
                else
                {
                    typeKnowledge = typeKnowledges[index];
                }
                
                var score = typeKnowledge.ScoreForHowWellOtherTypeFitsThis(otherTypeKnowledge);
                if (score == null)
                {
                    return null;
                }

                c += (int)score;
            }

            return c;
        }
    }
}