namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeResult : ITypeResult
    {
        private const string ClassPrefix = "element";

        private readonly string additionalString;
        private readonly Type type;
        public ITypeResult BaseType { get; private set; }

        public TypeResult(Type type, string additionalString)
        {
            this.type = type;
            this.additionalString = additionalString;
            if (type.BaseType != null)
            {
                this.BaseType = new TypeResult(type.BaseType);
            }
        }

        public TypeResult(Type type)
        {
            this.type = type;
            if (type.BaseType != null)
            {
                this.BaseType = new TypeResult(type.BaseType);
            }
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

        public string Name => this.type.Name;

        public string Namespace => this.type.Namespace;

        public bool IsClass => this.type.IsClass;

        public string FullName => this.type.FullName;

        public IEnumerable<ScopeElement> GetScopeElements()
        {
            var list = new List<ScopeElement>();

            if (this.type.BaseType != null)
            {
                list = this.BaseType.GetScopeElements().ToList();
            }

            IEnumerable<MemberInfo> methods = this.GetMethodsOfType(type);
            IEnumerable<MemberInfo> objectMethods = this.GetMethodsOfType(typeof(object));
            foreach (MemberInfo method in methods.Where(m => !objectMethods.Any(om => om.Name.Equals(m.Name))))
            {
                list.Add(new ScopeElement(method.Name)
                {
                    ClassPrefix = ClassPrefix,
                    IsFromClass = true,
                });
            }
            foreach (MemberInfo member in type.GetMembers()) // Variables and properties
            {
                if (!member.MemberType.Equals(MemberTypes.Method) && !member.MemberType.Equals(MemberTypes.Constructor))
                {
                    list.Add(new ScopeElement(member.Name)
                    {
                        ClassPrefix = ClassPrefix,
                        IsFromClass = true
                    });
                }
            }

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
            }

            return list;
        }

        private IEnumerable<MemberInfo> GetMethodsOfType(Type type)
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