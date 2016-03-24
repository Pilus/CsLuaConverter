namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeKnowledge
    {
        private Type type;

        public TypeKnowledge(Type type)
        {
            this.type = type;
        }

        public TypeKnowledge(MemberInfo member)
        {
            this.type = this.GetTypeFromMember(member);
        }

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
            var members = this.type.GetMember(name);

            return members.Select(m => new TypeKnowledge(m)).ToArray();
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