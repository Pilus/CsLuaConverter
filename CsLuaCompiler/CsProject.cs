namespace CsLuaCompiler
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;
    using CsLuaAttributes;
    using System.Reflection;

    internal class CsProject
    {
        private readonly Assembly assembly;
        public readonly Project CodeProject;
        public string Name;
        private readonly List<CustomAttributeData> customAttributes;
        public readonly ProjectType ProjectType;
        public CsLuaAddOnAttribute CsLuaAddOnAttribute;

        public CsProject(Project project)
        {
            this.Name = project.Name;
            this.CodeProject = project;

            this.assembly = Assembly.LoadFile(this.CodeProject.OutputFilePath);

            this.customAttributes = this.assembly.CustomAttributes
                .Where(att => att.AttributeType.Namespace.Equals("CsLuaAttributes")).ToList();

            this.ProjectType = this.DetermineProjectType(); 
        }


        public static Dictionary<string, NameSpace> GetStructuredSyntaxTree(ProjectType type, Project project)
        {
            if (type.Equals(ProjectType.CsLuaAddOn) || type.Equals(ProjectType.CsLuaLibrary))
            {
                if (Debugger.IsAttached)
                {
                    return GetNameSpaces(project);
                }
                
                try
                {
                    return GetNameSpaces(project);
                }
                catch (Exception ex)
                {

                    throw new WrappingException(string.Format("In project: {0}.", project.Name), ex);
                }
            }
            return null;
        }

        // TODO: Move this
        private static Dictionary<string, NameSpace> GetNameSpaces(Project project)
        {
            IEnumerable<Document> docs = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var nameSpaces = new Dictionary<string, NameSpace>();
            foreach (Document document in docs)
            {
                NameSpacePart nameSpacePart = new SyntaxAnalyser().AnalyseDocument(document);
                if (nameSpaces.ContainsKey(nameSpacePart.FullName.First()))
                {
                    nameSpaces[nameSpacePart.FullName.First()].AddPart(nameSpacePart);
                }
                else
                {
                    nameSpaces[nameSpacePart.FullName.First()] = new NameSpace(nameSpacePart, 1);
                }
            }
            return nameSpaces;
        }

        private ProjectType DetermineProjectType()
        {
            if (this.customAttributes.Any(att => att.AttributeType == typeof(CsLuaLibraryAttribute)))
            {
                return ProjectType.CsLuaLibrary;
            }

            var csLuaAddOnAttribute = this.assembly.GetTypes()
                .Select(t => System.Attribute.GetCustomAttribute(t, typeof(CsLuaAddOnAttribute)) as CsLuaAddOnAttribute).FirstOrDefault(att => att != null);

            if (csLuaAddOnAttribute != null)
            {
                this.CsLuaAddOnAttribute = csLuaAddOnAttribute;
                return ProjectType.CsLuaAddOn;
            }

            var fileInfo = new FileInfo(this.GetProjectPath() + "\\" + this.CodeProject.Name + ".toc");
            if (fileInfo.Exists) return ProjectType.LuaAddOn;

            var dir = new DirectoryInfo(this.GetProjectPath());
            return dir.GetFiles("*.lua", SearchOption.AllDirectories).Length > 0 ? ProjectType.LuaLibrary : ProjectType.Ignored;
        }

        public string GetProjectPath()
        {
            var projectFile = new FileInfo(this.CodeProject.FilePath);
            return projectFile.Directory.FullName;
        }

        public IEnumerable<string> GetReferences()
        {
            return this.assembly.GetReferencedAssemblies().Select(r => r.Name).Where(name => !name.Equals("mscorlib"));
        }


        public bool RequiresCsLuaMetaHeader()
        {
            return this.customAttributes.Any(att => att.ToString().Equals("CsLua.Attributes.RequiresCsLuaHeader"));
        }
    }
}