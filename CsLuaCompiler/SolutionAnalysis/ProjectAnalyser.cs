namespace CsLuaCompiler.SolutionAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsLuaAttributes;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;

    public static class ProjectAnalyser
    {
        public static ProjectInfo AnalyzeProject(Project project)
        {
            var assembly = Assembly.LoadFile(project.OutputFilePath);
            var csLuaAddOnAttribute = GetCsLuaAddOnAttribute(assembly);
            var projectPath = GetProjectPath(project);

            return new ProjectInfo()
            {
                Name = project.Name,
                Project = project,
                ProjectType = DetermineProjectType(project, assembly, csLuaAddOnAttribute, projectPath),
                RequiresCsLuaMetaHeader = RequiresCsLuaMetaHeader(assembly),
                CsLuaAddOnAttribute = csLuaAddOnAttribute,
                ProjectPath = projectPath,
                ReferencedProjects = GetReferences(assembly),
            };
        }

        private static CsLuaAddOnAttribute GetCsLuaAddOnAttribute(Assembly assembly)
        {
            return assembly.GetTypes()
                .Select(t => System.Attribute.GetCustomAttribute(t, typeof(CsLuaAddOnAttribute)) as CsLuaAddOnAttribute).FirstOrDefault(att => att != null);
        }

        private static ProjectType DetermineProjectType(Project project, Assembly assembly, CsLuaAddOnAttribute csLuaAddOnAttribute, string projectPath)
        {
            
            var customAttributes = assembly.CustomAttributes
                .Where(att => att.AttributeType.Namespace.Equals("CsLuaAttributes")).ToList();
            if (customAttributes.Any(att => att.AttributeType == typeof(CsLuaLibraryAttribute)))
            {
                return ProjectType.CsLuaLibrary;
            }


            if (csLuaAddOnAttribute != null)
            {
                return ProjectType.CsLuaAddOn;
            }

            var fileInfo = new FileInfo(this.GetProjectPath() + "\\" + project.Name + ".toc");
            if (fileInfo.Exists) return ProjectType.LuaAddOn;

            var dir = new DirectoryInfo(this.GetProjectPath());
            return dir.GetFiles("*.lua", SearchOption.AllDirectories).Length > 0 ? ProjectType.LuaLibrary : ProjectType.Ignored;
        }

        private static string GetProjectPath(Project project)
        {
            var projectFile = new FileInfo(project.FilePath);
            return projectFile.Directory.FullName;
        }

        private static IList<string> GetReferences(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(r => r.Name).Where(name => !name.Equals("mscorlib")).ToList();
        }


        private static bool RequiresCsLuaMetaHeader(Assembly assembly)
        {
            return assembly.CustomAttributes.Any(att => att.AttributeType == typeof(RequiresCsLuaHeader));
        }
    }
}