namespace CsLuaConverter.ProjectAnalysis
{
    using System.Collections.Generic;
    using CsLuaFramework.Attributes;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;

    public class ProjectInfo
    {
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public CsLuaAddOnAttribute CsLuaAddOnAttribute { get; set; }
        public Project Project { get; set; }
        public string ProjectPath { get; set; }
        public bool RequiresCsLuaMetaHeader { get; set; }
        public IList<string> ReferencedProjects {get; set; }

        public bool IsAddOn()
        {
            return this.ProjectType == ProjectType.CsLuaAddOn || this.ProjectType == ProjectType.LuaAddOn;
        }

        public bool IsCsLua()
        {
            return this.ProjectType == ProjectType.CsLuaAddOn || this.ProjectType == ProjectType.CsLuaLibrary;
        }

        public bool IsLibrary()
        {
            return this.ProjectType == ProjectType.CsLuaLibrary || this.ProjectType == ProjectType.LuaLibrary;
        }

    }
}