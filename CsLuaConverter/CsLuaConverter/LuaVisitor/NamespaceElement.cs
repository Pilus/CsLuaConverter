namespace CsLuaConverter.LuaVisitor
{
    using System.Collections.Generic;
    using CodeElementAnalysis;

    public class NamespaceElement
    {
        public List<UsingDirective> Usings = new List<UsingDirective>();

        public List<AttributeList> Attributes = new List<AttributeList>();

        public string NamespaceLocation;

        public BaseElement Element;
    }
}