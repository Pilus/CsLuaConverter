namespace CsLuaConverter.LuaVisitor
{
    using System.Collections.Generic;
    using System.Linq;

    public class Namespace
    {
        private readonly string parentFullName;

        public readonly bool IsRoot;
        public readonly string Name;
        public readonly List<Namespace> SubNamespaces;
        public readonly List<NamespaceElement> Elements;

        public Namespace(string name, string parentFullName)
        {
            this.Name = name;
            this.IsRoot = (parentFullName == null);
            this.parentFullName = parentFullName;
            this.FullName = (this.parentFullName == null) ? this.Name : this.parentFullName + "." + this.Name;
            this.SubNamespaces = new List<Namespace>();
            this.Elements = new List<NamespaceElement>();
        }

        public string FullName { get; }

        public void Add(NamespaceElement namespaceElement)
        {
            if (namespaceElement.NamespaceLocation.Equals(this.FullName))
            {
                this.Elements.Add(namespaceElement);
                return;
            }

            var matchingNamespace = this.SubNamespaces.SingleOrDefault(s => s.Fits(namespaceElement));

            if (matchingNamespace == null)
            {
                matchingNamespace = new Namespace(namespaceElement.NamespaceLocation.Replace(this.FullName + ".", "").Split('.').First(), this.FullName);
                this.SubNamespaces.Add(matchingNamespace);
            }

            matchingNamespace.Add(namespaceElement);
        }

        public bool Fits(NamespaceElement namespaceElement)
        {
            return this.FullName.Equals(namespaceElement.NamespaceLocation) ||
                       namespaceElement.NamespaceLocation.StartsWith(this.FullName + ".");
        }
    }
}