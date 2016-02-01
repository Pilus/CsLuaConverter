namespace CsLuaFramework.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]

    public sealed class MetadataTagAttribute : Attribute
    {
        public string Key;
        public string Value;

        public MetadataTagAttribute(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}