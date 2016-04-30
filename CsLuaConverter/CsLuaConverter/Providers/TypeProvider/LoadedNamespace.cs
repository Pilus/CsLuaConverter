namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TypeKnowledgeRegistry;

    public class LoadedNamespace
    {
        public LoadedNamespace(string name)
        {
            this.Name = name;
        }

        public LoadedNamespace Upsert(string nameSpaceName)
        {
            if (!this.SubNamespaces.ContainsKey(nameSpaceName))
            {
                this.SubNamespaces.Add(nameSpaceName, new LoadedNamespace(nameSpaceName));
            }
            return this.SubNamespaces[nameSpaceName];
        }

        public void Upsert(Type type)
        {
            var name = StripGenerics(type.Name);
            if (!this.Types.ContainsKey(name))
            {
                this.Types[name] = new List<LoadedType>();
            }

            this.Types[name].Add(new LoadedType(type));
            this.AddPossibleExtensionMethods(type);
        }

        private static string StripGenerics(string name)
        {
            return name.Split('`').First();
        }

        public LoadedNamespace TryGetNamespace(IList<string> names)
        {
            var name = StripGenerics(names.First());
            if (this.SubNamespaces.ContainsKey(name))
            {
                return names.Count > 1 ? this.SubNamespaces[name].TryGetNamespace(names.Skip(1).ToList()) : this.SubNamespaces[name];
            }
            return null;
        }

        public TypeKnowledge[] GetExtensionMethods(Type type, string name)
        {
            return !this.ExtensionMethods.ContainsKey(name) ? new TypeKnowledge[] {} :
            this.ExtensionMethods[name]
                .Where(extension => GetMatchingType(extension.Item1, type) != null)
                .Select(v => v.Item2.GetTypeKnowledgeOnExtensionOfType(type)).ToArray();
        }

        private static Type GetMatchingType(Type extensionType, Type type)
        {
            if (TypeNameAndNamespaceAreIdentical(extensionType, type))
            {
                return type;
            }

            foreach (var t in type.GetInterfaces().Select(inheritiedInterfaces => GetMatchingType(extensionType, inheritiedInterfaces)).Where(t => t != null))
            {
                return t;
            }

            return type.BaseType != null ? GetMatchingType(extensionType, type.BaseType) : null;
        }

        private static bool TypeNameAndNamespaceAreIdentical(Type a, Type b)
        {
            return a.Name == b.Name && a.Namespace == b.Namespace;
        }

        private void AddPossibleExtensionMethods(Type type)
        {
            if (!IsExtensionClass(type))
            {
                return;
            }

            var extensionMethods = type.GetMethods().Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof (ExtensionAttribute)));

            foreach (var method in extensionMethods)
            {
                var name = method.Name;
                var extensionType = method.GetParameters().First().ParameterType;
                this.AddExtensionMethod(name, extensionType, new ExtensionMethod(method));
            }
        }

        private static bool IsExtensionClass(Type type)
        {
            return type.CustomAttributes.Any(a => a.AttributeType == typeof (ExtensionAttribute));
        }

        public string Name;
        public Dictionary<string, LoadedNamespace> SubNamespaces = new Dictionary<string, LoadedNamespace>();
        public Dictionary<string, IList<LoadedType>> Types = new Dictionary<string, IList<LoadedType>>();

        private readonly Dictionary<string, IList<Tuple<Type, ExtensionMethod>>> ExtensionMethods = new Dictionary<string, IList<Tuple<Type, ExtensionMethod>>>();

        private void AddExtensionMethod(string name, Type extensionType, ExtensionMethod typeKnowledgeForMethod)
        {
            if (!this.ExtensionMethods.ContainsKey(name))
            {
                this.ExtensionMethods.Add(name, new List<Tuple<Type, ExtensionMethod>>());
            }

            if (this.ExtensionMethods[name].All(t => !t.Item2.Equals(typeKnowledgeForMethod)))
            { 
                this.ExtensionMethods[name].Add(new Tuple<Type, ExtensionMethod>(extensionType, typeKnowledgeForMethod));
            }
        }
    }
}