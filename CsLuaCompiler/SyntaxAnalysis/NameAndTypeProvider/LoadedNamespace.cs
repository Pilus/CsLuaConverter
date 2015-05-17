namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            this.Types.Add(StripGenerics(type.Name), new LoadedType(type));
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


        public string Name;
        public Dictionary<string, LoadedNamespace> SubNamespaces = new Dictionary<string, LoadedNamespace>();
        public Dictionary<string, LoadedType> Types = new Dictionary<string, LoadedType>();
    }
}