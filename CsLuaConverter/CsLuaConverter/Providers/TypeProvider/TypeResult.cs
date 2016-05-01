namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TypeKnowledgeRegistry;

    public class TypeResult : ITypeResult
    {
        private const string ClassPrefix = "element";

        private readonly string additionalString;
        private readonly Type type;
        private readonly string nameWithoutGeneric;
        public ITypeResult BaseType { get; }

        public TypeResult(Type type, string additionalString)
        {
            this.type = type;
            this.additionalString = additionalString;
            if (type.BaseType != null)
            {
                this.BaseType = new TypeResult(type.BaseType);
            }

            this.nameWithoutGeneric = this.type.Name.Split('`').First();
        }

        public TypeResult(Type type)
        {
            this.type = type;
            if (type.BaseType != null)
            {
                this.BaseType = new TypeResult(type.BaseType);
            }

            this.nameWithoutGeneric = this.type.Name.Split('`').First();
        }

        private static string StripGenericsFromType(string name)
        {
            return name.Split('`').First();
        }

        public override string ToString()
        {
            var genericStrippedFullName = StripGenericsFromType(this.type.FullName);
            if (string.IsNullOrEmpty(this.additionalString))
            {
                return genericStrippedFullName;
            }

            return genericStrippedFullName + "." + this.additionalString;
        }


        public bool IsInterface => this.type.IsInterface;

        public int NumGenerics => this.type.GetGenericArguments().Length;

        public string Name => this.nameWithoutGeneric;

        public string Namespace => this.type.Namespace;

        public bool IsClass => this.type.IsClass;

        public string FullNameWithoutGenerics => this.type.Namespace + "." + this.nameWithoutGeneric;

        public Type TypeObject => this.type;

        public IEnumerable<ScopeElement> GetScopeElements()
        {
            var list = new List<ScopeElement>();

            if (this.type.BaseType != null)
            {
                list = this.BaseType.GetScopeElements().ToList();
            }

            IEnumerable<MethodInfo> methods = this.GetMethodsOfType(this.type);
            IEnumerable<MethodInfo> objectMethods = this.GetMethodsOfType(typeof(object));

            foreach (var method in methods.Where(m => !objectMethods.Any(om => om.Name.Equals(m.Name))))
            {
                list.Add(new ScopeElement(method.Name, new TypeKnowledge(method))
                {
                    ClassPrefix = ClassPrefix,
                    IsFromClass = true,
                });
            }

            foreach (var member in this.GetMembersOfType(this.type)) // Variables and properties
            {
                if (   !member.MemberType.Equals(MemberTypes.Method) 
                    && !member.MemberType.Equals(MemberTypes.Constructor) 
                    && !member.MemberType.Equals(MemberTypes.Event)
                    && !member.MemberType.Equals(MemberTypes.NestedType)
                    && !member.Name.Contains("<")
                    && !member.Name.StartsWith("__")
                    )
                {
                    list.Add(new ScopeElement(member.Name, new TypeKnowledge(member))
                    {
                        ClassPrefix = ClassPrefix,
                        IsFromClass = true
                    });
                }
            }

            /*
            foreach (var property in type.GetRuntimeProperties())
            {
                list.Add(new ScopeElement(property.Name)
                {
                    ClassPrefix = ClassPrefix,
                    IsFromClass = true
                });
            }

            foreach (var field in type.GetRuntimeFields())
            {
                if (!field.Name.EndsWith("__BackingField"))
                {
                    list.Add(new ScopeElement(field.Name)
                    {
                        ClassPrefix = ClassPrefix,
                        IsFromClass = true
                    });
                }
            } */

            return list;
        }

        private IEnumerable<MemberInfo> GetMembersOfType(Type type)
        {
            var all =    type.GetMembers(BindingFlags.Public | BindingFlags.Instance).ToList();
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));
            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Static));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));

            return all;
        }

        private IEnumerable<MethodInfo> GetMethodsOfType(Type type)
        {
            List<MethodInfo> publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();

            List<MethodInfo> privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            publicMethods.AddRange(privateMethods);
            List<MethodInfo> staticPublicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
            publicMethods.AddRange(staticPublicMethods);
            List<MethodInfo> staticPrivateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static).ToList();
            publicMethods.AddRange(staticPrivateMethods);

            return publicMethods;
        }

    }
}