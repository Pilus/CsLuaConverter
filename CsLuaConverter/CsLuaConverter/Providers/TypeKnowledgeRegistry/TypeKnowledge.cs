namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using GenericsRegistry;
    using TypeProvider;

    [DebuggerDisplay("TypeKnowledge: {type}")]
    public class TypeKnowledge : IKnowledge
    {
        public static IProviders Providers;
        private readonly Type type;
        private readonly bool restrictToStatic;
        public TypeKnowledge(Type type, bool restrictToStatic = false)
        {
            this.type = type;
            this.restrictToStatic = restrictToStatic;
            /*
            if (!this.type.IsGenericParameter) return;

            var genericType = Providers.GenericsRegistry.GetType(this.type.Name);
            var genericScope = Providers.GenericsRegistry.GetGenericScope(this.type.Name);
            if (genericType == null || genericScope == GenericScope.MethodDeclaration)
            {
                return;
            }

            this.type = genericType;
            this.restrictToStatic = restrictToStatic; */
        }

        public TypeKnowledge(MemberInfo member, bool skipFirstInputArg = false)
        {
            this.type = this.GetTypeFromMember(member, skipFirstInputArg);

            if (this.type.IsGenericParameter)
            {
                this.type = Providers.GenericsRegistry.GetType(this.type.Name);
            }
        }

        public bool IsParams { get; set; }
        public Type[] MethodGenerics { get; private set; }

        public string Name => this.type.Name.Split('`').First();

        public bool IsGenericParameter => this.type.IsGenericParameter;

        public bool IsGenericType => this.type?.IsGenericType ?? false;

        public override int GetHashCode()
        {
            if (this.type != null)
            {
                return this.type.GetHashCode() + 43;
            }

            return base.GetHashCode();
        }

        public IKnowledge[] GetTypeKnowledgeForSubElement(string str, IProviders providers)
        {
            var type = this.type;

            if (type.IsGenericParameter)
            {
                if (providers.GenericsRegistry.IsGeneric(type.Name))
                {
                    type = providers.GenericsRegistry.GetType(type.Name);
                }
            }

            var members =
                GetMembers(type, this.restrictToStatic, str)
                    .GroupBy(m => m.GetHashCode())
                    .Select(g => g.First())
                    .ToArray();
            var extensions = providers.TypeProvider.GetExtensionMethods(type, str);
            var membersIncludingExtensions = new []{ members, extensions}.SelectMany(v => v).ToArray();
            
            if (!membersIncludingExtensions.Any())
            {
                throw new Exception($"Member {str} not found on element {this.type.Name}.");
            }

            return membersIncludingExtensions;
        }

        public MethodKnowledge[] GetConstructors()
        {
            if (typeof(Delegate).IsAssignableFrom(this.type))
            {
                return new[] {new MethodKnowledge(false, null, new Type[] {}, new [] { this.type}), new MethodKnowledge() };
            }

            var cstors = GetMembersOfType(this.type, true, true) //this.type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.MemberType.Equals(MemberTypes.Constructor))
                .OfType<MethodBase>()
                .Select(m => new MethodKnowledge(m))
                .ToArray();

            return cstors.Length > 0 ? cstors : new[] {new MethodKnowledge()};
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
            return GetIndexerValueType(this.type);
        }

        public static TypeKnowledge GetIndexerValueType(Type type)
        {
            if (type.IsArray)
            {
                return new TypeKnowledge(typeof(int));
            }

            var indexParameters = type.GetProperties().SingleOrDefault(p => p.GetIndexParameters().Length > 0)?.GetIndexParameters().SingleOrDefault();

            if (indexParameters == null)
            {
                if (type.IsInterface)
                {
                    var interfaces = type.GetInterfaces();
                    foreach (var inheritedInterface in interfaces)
                    {
                        var indexer = GetIndexerValueType(inheritedInterface);
                        if (indexer != null)
                        {
                            return indexer;
                        }
                    }

                    return null;
                }

                return type.BaseType != null ? GetIndexerValueType(type.BaseType) : null;
            }

            return new TypeKnowledge((indexParameters.Member as PropertyInfo).PropertyType);
        }

        public TypeKnowledge GetAsArrayType()
        {
            return new TypeKnowledge(this.type.MakeArrayType());
        }

        public TypeKnowledge GetArrayGeneric()
        {
            if (!this.type.IsArray)
            {
                return null;
            }

            return new TypeKnowledge(this.type.GetInterface(typeof(IEnumerable<object>).Name).GetGenericArguments().Single());
        }

        public string GetFullName()
        {
            if (this.type.IsArray)
            {
                return typeof(Array).FullName;
            }

            return this.type.Namespace + "." + this.type.Name.Split('`').First();
        }

        public TypeKnowledge[] GetGenerics()
        {
            if (this.type.IsArray)
            {
                return new [] { new TypeKnowledge( this.type.GetElementType()) };
            }

            //return this.type.GetGenericArguments().Select(t => !t.IsGenericParameter || Providers.GenericsRegistry.IsGeneric(t.Name) ?  new TypeKnowledge(t) : new TypeKnowledge(t.Name)).ToArray();
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

        private static IKnowledge[] GetMembers(Type type, bool restrictToStatic, string name)
        {
            var members = GetMembersOfType(type).Distinct().Where(e => e.Name == name && (restrictToStatic == false || IsMemberStatic(e)));

            return members.Select<MemberInfo, IKnowledge>(m =>
            {
                var method = m as MethodBase;
                if (method != null)
                {
                    return new MethodKnowledge(method);
                }

                return new TypeKnowledge(m);
            }).ToArray();
        }

        private static bool IsMemberStatic(MemberInfo member)
        {
            var method = member as MethodInfo;
            var field = member as FieldInfo;

            return method?.IsStatic ?? field?.IsStatic ?? false;
        }

        private static IEnumerable<MemberInfo> GetMembersOfType(Type type, bool excludeBase = false, bool excludeStatic = false)
        {
            var all = new List<MemberInfo>();

            if (type.IsInterface)
            {
                all.AddRange(GetMembersOfType(typeof(object), excludeStatic: excludeStatic));
                all.AddRange(type.GetInterfaces().SelectMany(i => GetMembersOfType(i)));
            }

            var _base = type.BaseType;
            while (_base != null && excludeBase == false)
            {
                all.AddRange(_base.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));
                if (!excludeStatic)
                { 
                    all.AddRange(_base.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));
                    all.AddRange(_base.GetMembers(BindingFlags.Public | BindingFlags.Static));
                }

                _base = _base.BaseType;
            }

            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Instance));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));

            if (!excludeStatic)
            {
                all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Static));
                all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));
            }

            return all;
        }

        private static Dictionary<string, Type> Translations = new Dictionary<string, Type>()
        {
            { "System.Object&", typeof(object) },
        };

        private Type GetTypeFromMember(MemberInfo member, bool skipFirstInputArg)
        {
            var methodInfo = member as MethodBase;
            if (methodInfo != null)
            {
                var returnType = (methodInfo as MethodInfo)?.ReturnType ?? member.DeclaringType;
                var parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).Skip(skipFirstInputArg ? 1 : 0).ToList();

                var type = ActionFuncTypes.GetAction(parameterTypes.Count);
                if (returnType != typeof(void))
                {
                    type = ActionFuncTypes.GetFunc(parameterTypes.Count + 1);
                    parameterTypes.Add(returnType);
                }

                this.IsParams = MethodHasParams(methodInfo);

                if (methodInfo.IsGenericMethod)
                {
                    this.MethodGenerics = methodInfo.GetGenericArguments();
                }

                if (parameterTypes.Count == 0)
                {
                    return typeof(Action);
                }

                parameterTypes = parameterTypes.Select(t => Translations.ContainsKey(t?.FullName ?? string.Empty) ? Translations[t.FullName] : t).ToList();

                if (parameterTypes.Any(t => t.IsByRef))
                {
                    parameterTypes = parameterTypes.Select(t => t.IsByRef ? t.GetElementType() : t).ToList();
                }

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
                throw new Exception("Unknown member type: " + member.GetType().Name);
            }

            return fieldInfo.FieldType;
        }

        public static TypeKnowledge ConstructLambdaType(TypeKnowledge[] inputs, TypeKnowledge returnArg)
        {
            if (returnArg == null)
            {
                if (inputs.Length == 0)
                {
                    return new TypeKnowledge(typeof (Action));
                }
                else
                {
                    var actionType = ActionFuncTypes.GetAction(inputs.Length);
                    return new TypeKnowledge(actionType.MakeGenericType(inputs.Select(tk => tk.GetTypeObject()).ToArray()));
                }
            }

            var funcType = ActionFuncTypes.GetFunc(inputs.Length + 1);
            var generics = inputs.Select(tk => tk.GetTypeObject()).Union(new[] {returnArg.GetTypeObject()}).ToArray();
            return new TypeKnowledge(funcType.MakeGenericType(generics));
        }

        public static bool MethodHasParams(MethodBase mi)
        {
            var lastParameter = mi.GetParameters().LastOrDefault();

            return lastParameter?.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() ?? false;
        }

        public bool IsArray()
        {
            return this.type.IsArray;
        }
    }
}