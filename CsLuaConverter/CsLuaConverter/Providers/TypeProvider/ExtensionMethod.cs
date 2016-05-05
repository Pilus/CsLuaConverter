namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TypeKnowledgeRegistry;

    public class ExtensionMethod
    {
        private readonly MethodInfo methodInfo;

        public ExtensionMethod(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }


        public MethodKnowledge GetKnowledgeOnExtensionOfType(Type type)
        {
            //var parameters = this.methodInfo.GetParameters();
            //var generics = SubtractGenerics(parameters.First().ParameterType, type).ToDictionary(v => v.Item1, v => v.Item2);

            return new MethodKnowledge(this.methodInfo);
        }

        private Type GetType(Dictionary<string, Type> generics)
        {
            var parameterTypes = this.methodInfo.GetParameters().Select(p => p.ParameterType).Skip(1).ToList();

            var type = ActionFuncTypes.GetAction(parameterTypes.Count);
            if (this.methodInfo.ReturnType != typeof(void))
            {
                type = ActionFuncTypes.GetFunc(parameterTypes.Count + 1);
                parameterTypes.Add(this.methodInfo.ReturnType);
            }

            if (parameterTypes.Count == 0)
            {
                return type;
            }

            parameterTypes = parameterTypes.Select(t => t.FullName == "System.Object&" ? typeof(object) : t).ToList();

            return type.MakeGenericType(parameterTypes.ToArray().Select(t => ApplyGenerics(t, generics)).ToArray());
        }

        private static Type ApplyGenerics(Type type, Dictionary<string, Type> generics)
        {
            if (type.IsGenericParameter)
            {
                return generics.ContainsKey(type.Name) ? generics[type.Name] : type;
            }

            if (!type.IsGenericType) return type;

            var genericTypes = type.GetGenericArguments().Select(g => ApplyGenerics(g, generics)).ToArray();
            var def = type.GetGenericTypeDefinition();
            return def.MakeGenericType(genericTypes);
        }

        private static Tuple<string, Type>[] SubtractGenerics(Type parameter, Type type)
        {
            if (parameter.IsGenericParameter)
            {
                return new[] {new Tuple<string, Type>(parameter.Name, type)};
            }

            if (!parameter.IsGenericType) return new Tuple<string, Type>[] {};

            type = As(type, parameter);

            var list = new List<Tuple<string, Type>>();

            var parameterGenerics = parameter.GetGenericArguments();
            var typeGenerics = type.GetGenericArguments();

            for (var index = 0; index < parameterGenerics.Length; index++)
            {
                list.AddRange(SubtractGenerics(parameterGenerics[index], typeGenerics[index]));
            }

            return list.ToArray();
        }

        private static Type As(Type type, Type targetType)
        {
            foreach (var t in type.GetInterfaces().Select(implementedInterface => As(implementedInterface, targetType)).Where(t => t != null))
            {
                return t;
            }

            while (type != null)
            {
                if (type.Name == targetType.Name && type.Namespace == targetType.Namespace)
                {
                    return type;
                }

                type = type.BaseType;
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            return !(obj is ExtensionMethod) || this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.methodInfo.GetHashCode();
        }

        public override string ToString()
        {
            return this.methodInfo.ToString();
        }
    }
}