namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    [DebuggerDisplay("TypeKnowledge: {type}")]
    public class TypeKnowledge
    {
        private readonly Type type;

        public TypeKnowledge(Type type)
        {
            this.type = type;
        }

        public TypeKnowledge(MemberInfo member)
        {
            this.type = this.GetTypeFromMember(member);
        }

        public bool IsParams { get; private set; }

        public TypeKnowledge GetTypeKnowledgeForSubElement(string[] strings)
        {
            var members = this.GetMembers(strings.First());

            if (members.Count() == 1)
            {
                return members.Single();
            }

            if (!members.Any())
            {
                throw new Exception($"Member {strings.First()} not found on element {this.type.Name}.");
            }

            throw new NotImplementedException();
        }

        public TypeKnowledge GetConstructor()
        {
            return this.type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.MemberType.Equals(MemberTypes.Constructor))
                .Select(m => new TypeKnowledge(m))
                .FirstOrDefault() ?? new TypeKnowledge(typeof(Action));
        }

        public TypeKnowledge GetTypeKnowledgeForSubElement(string str)
        {
            return this.GetTypeKnowledgeForSubElement(str.Split('.'));
        }


        public TypeKnowledge GetEnumeratorType()
        {
            if (!typeof(IEnumerable).IsAssignableFrom(this.type))
            {
                throw new Exception("Attempting to get enumerator on a non IEnumerable type.");
            }

            var enumerableType = this.type.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Where(t => t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(t => t.GetGenericArguments()[0])
                .FirstOrDefault();

            return new TypeKnowledge(enumerableType);
        }

        public TypeKnowledge GetIndexerIndexType()
        {
            var indexParameters = this.type.GetProperties().Single(p => p.GetIndexParameters().Length > 0).GetIndexParameters().Single();

            return new TypeKnowledge(indexParameters.ParameterType);
        }


        public TypeKnowledge GetIndexerValueType()
        {
            var indexParameters = this.type.GetProperties().Single(p => p.GetIndexParameters().Length > 0).GetIndexParameters().Single();

            return new TypeKnowledge((indexParameters.Member as PropertyInfo).PropertyType);
        }

        public TypeKnowledge GetAsArrayType()
        {
            return new TypeKnowledge(this.type.MakeArrayType());
        }

        public string GetFullName()
        {
            if (this.type.IsArray)
            {
                return typeof(Array).FullName;
            }

            return this.type.FullName.Split('`').First();
        }

        public TypeKnowledge[] GetGenerics()
        {
            if (this.type.IsArray)
            {
                return new [] { new TypeKnowledge( this.type.GetElementType()) };
            }

            return this.type.GetGenericArguments().Select(t => new TypeKnowledge(t)).ToArray();
        }

        public TypeKnowledge CreateWithGenerics(TypeKnowledge[] generics)
        {
            return new TypeKnowledge(this.type.MakeGenericType(generics.Select(g => g.GetTypeObject()).ToArray()));
        }

        public Type GetTypeObject()
        {
            return this.type;
        }

        private TypeKnowledge[] GetMembers(string name)
        {
            var members = GetMembersOfType(this.type).Distinct().Where(e => e.Name == name);

            return members.Select(m => new TypeKnowledge(m)).ToArray();
        }

        private static IEnumerable<MemberInfo> GetMembersOfType(Type type)
        {
            var all = new List<MemberInfo>();

            var _base = type.BaseType;
            while (_base != null)
            {
                all.AddRange(_base.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));
                all.AddRange(_base.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));
                all.AddRange(_base.GetMembers(BindingFlags.Public | BindingFlags.Static));
                _base = _base.BaseType;
            }

            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Instance));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));
            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Static));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));

            return all;
        }

        private Type GetTypeFromMember(MemberInfo member)
        {
            var methodInfo = member as MethodInfo;
            if (methodInfo != null)
            {
                var parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToList();

                var type = Actions[parameterTypes.Count];
                if (methodInfo.ReturnType != typeof(void))
                {
                    type = Funcs[parameterTypes.Count];
                    parameterTypes.Add(methodInfo.ReturnType);
                }

                this.IsParams = MethodHasParams(methodInfo);

                if (parameterTypes.Count == 0)
                {
                    return typeof(Action);
                }

                parameterTypes = parameterTypes.Select(t => t.FullName == "System.Object&" ? typeof (object) : t).ToList();

                return type.MakeGenericType(parameterTypes.ToArray());
            }

            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            var fieldInfo = member as FieldInfo;
            if (fieldInfo == null)
            {
                throw new Exception("Unknown member type: " + fieldInfo.GetType().Name);
            }

            return fieldInfo.FieldType;
        }

        public static bool MethodHasParams(MethodInfo mi)
        {
            var lastParameter = mi.GetParameters().LastOrDefault();

            return lastParameter?.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() ?? false;
        }

        private static readonly List<Type> Actions = new List<Type>()
        {
            typeof (Action),
            typeof (Action<>),
            typeof (Action<,>),
            typeof (Action<,,>),
            typeof (Action<,,,>),
            typeof (Action<,,,,>),
            typeof (Action<,,,,,>),
        };

        private static readonly List<Type> Funcs = new List<Type>()
        {
            typeof (Func<>),
            typeof (Func<,>),
            typeof (Func<,,>),
            typeof (Func<,,,>),
            typeof (Func<,,,,>),
            typeof (Func<,,,,,>),
        };

    }
}