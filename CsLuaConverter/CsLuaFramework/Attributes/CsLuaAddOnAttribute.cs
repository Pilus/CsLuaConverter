namespace CsLuaFramework.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]

    public sealed class CsLuaAddOnAttribute : Attribute
    {
        public string Name;
        public string Title;
        public int InterfaceVersion;

        public CsLuaAddOnAttribute(string name, string title, int interfaceVersion)
        {
            this.Name = name;
            this.Title = title;
            this.InterfaceVersion = interfaceVersion;
        }

        public string Notes { get; set; }

        public string[] Dependencies { get; set; }

        public string[] OptionalDeps { get; set; }

        public bool LoadOnDemand { get; set; }

        public string[] LoadWith { get; set; }

        public string[] LoadManagers { get; set; }

        public string[] SavedVariables { get; set; }

        public string[] SavedVariablesPerCharacter { get; set; }

        public DefaultState? DefaultState { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }
    }
}