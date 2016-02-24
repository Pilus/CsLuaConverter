namespace CsLuaConverter.Providers.TypeProvider
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
            var name = StripGenerics(type.Name);
            if (!this.Types.ContainsKey(name))
            {
                this.Types[name] = new List<LoadedType>();
            }

            this.Types[name].Add(new LoadedType(type));
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
        public Dictionary<string, IList<LoadedType>> Types = new Dictionary<string, IList<LoadedType>>();
    }
}