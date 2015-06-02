namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;

    internal static class SolutionHandler
    {
        public static bool IsAddOn(this CsProject project)
        {
            return project.ProjectType.Equals(ProjectType.CsLuaAddOn) || project.ProjectType.Equals(ProjectType.LuaAddOn);
        }

        public static CsProject GetProjectByName(this IEnumerable<CsProject> projects, string name)
        {
            return projects.FirstOrDefault(project => project.Name.Equals(name));
        }

        public static IEnumerable<CsProject> GetRootProjects(IEnumerable<CsProject> projects)
        {
            return projects.Where(project =>
                IsAddOn(project) &&
                !project.GetReferences().Any(referenceName => IsAddOn(GetProjectByName(projects, referenceName))));
        }

        public static void AddRange<T>(this IList<T> list, IList<T> range)
        {
            foreach (var item in range)
            {
                list.Add(item);
            }
        }

        public static IList<CsProject> GetReferenders(this CsProject project, IEnumerable<CsProject> projects)
        {
            return projects.Where(p => p.GetReferences().Contains(project.Name)).ToList();
        }

        public static void AddAddOnAndReferencesToList(CsProject project, IList<CsProject> nonDeployedProjects, IList<IDeployableAddOn> addOns, List<CodeFile> additionalFiles)
        {
            if (!project.IsAddOn())
            {
                throw new CompilerException("The project must be an addon.");
            }

            if (addOns.Any(addon => addon.Name.Equals(project.Name)))
            {
                // The addon is already in the list.
                return;
            }

            nonDeployedProjects.Remove(project);

            var luaFiles = additionalFiles ?? new List<CodeFile>();
            luaFiles.AddRange(project.GetLuaFiles());
            AddXmlToCodeFiles(project, luaFiles);
            var resources = GetResourceFiles(project.CodeProject);

            IEnumerable<CsProject> refProjects = project.GetReferences()
                .Select(name => nonDeployedProjects.GetProjectByName(name))
                .Where(proj => proj != null);

            foreach (var refName in project.GetReferences())
            {
                var refProject = nonDeployedProjects.GetProjectByName(refName);
                if (refProject == null)
                {
                    continue;
                }
                
                switch (refProject.ProjectType)
                {
                    case ProjectType.CsLuaLibrary:
                        luaFiles.AddRange(refProject.GetLuaFiles());
                        AddXmlToCodeFiles(refProject, luaFiles);
                        break;
                    case ProjectType.LuaLibrary:
                        luaFiles.AddRange(refProject.GetLuaFiles());
                        AddXmlToCodeFiles(refProject, luaFiles);
                        break;
                    default:
                        throw new System.Exception("Unknown project type.");
                }

                nonDeployedProjects.Remove(refProject);
            }

            foreach (var referender in project.GetReferenders(nonDeployedProjects))
            {
                switch (referender.ProjectType)
                {
                    case ProjectType.CsLuaAddOn:
                        AddAddOnAndReferencesToList(referender, nonDeployedProjects, addOns, null);
                        break;
                    case ProjectType.LuaAddOn:
                        if (!addOns.Any(addon => addon.Name.Equals(referender.Name)))
                        {
                            addOns.Add(new LuaAddOn(referender.Name, referender.GetProjectPath()));
                        }

                        break;
                    case ProjectType.LuaLibrary:
                    case ProjectType.CsLuaLibrary:
                        break;
                    default:
                        throw new System.Exception("Unknown project type.");
                }
            }

            addOns.Add(new AddOn(project.Name, project.Settings, luaFiles, resources));
        }

        public static IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution, IProviders nameProvider)
        {
            List<CsProject> csProjects =
                solution.Projects.Select(project => new CsProject(nameProvider, project)).ToList();

            var addOns = new List<IDeployableAddOn>();

            var rootAddOnProjects = GetRootProjects(csProjects);
            foreach (var rootProject in rootAddOnProjects)
            {
                AddAddOnAndReferencesToList(rootProject, csProjects.ToList(), addOns, new List<CodeFile>() { CsLuaMetaReader.GetMetaFile() });
            }

            return addOns;
        }


        private static readonly string[] ResourceFileExtensions = { "*.tga", "*.ttf", "*.blp", "*.mp3", "*.wav", "*.ogg", "*.txt", "*.xml" };

        private static IList<ResourceFile> GetResourceFiles(Project project)
        {
            var projectDir = new FileInfo(project.FilePath).Directory;
            return ResourceFileExtensions
                .SelectMany(ext => projectDir.GetFiles(ext, SearchOption.AllDirectories))
                .Where(info => !info.FullName.StartsWith(projectDir.FullName + "\\bin\\"))
                .Where(info => !info.FullName.StartsWith(projectDir.FullName + "\\obj\\"))
                .Select(info => new ResourceFile()
                {
                    FullPath = info.FullName,
                    RelativePath = info.FullName.Replace(projectDir.FullName + "\\", ""),
                })
                .ToList();
        }

        private static void AddXmlToCodeFiles(CsProject project, IList<CodeFile> codeFiles)
        {
            var projectDir = new FileInfo(project.CodeProject.FilePath).Directory;
            var xmlFiles = projectDir.GetFiles("*.xml", SearchOption.AllDirectories)
                .Where(info => !info.FullName.StartsWith(projectDir.FullName + "\\bin\\"))
                .Where(info => !info.FullName.StartsWith(projectDir.FullName + "\\obj\\"));
            string xmlContent = null;
            string xmlHeaderName = null;

            foreach (var xmlFile in xmlFiles)
            {
                using (var reader = new XmlTextReader(new FileStream(xmlFile.FullName, FileMode.Open)))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType != XmlNodeType.Element || reader.Name.ToLower() != "ui") continue;

                        if (xmlContent == null)
                        {
                            xmlHeaderName = reader.Name;

                            string attributes = string.Empty;
                            while (reader.MoveToNextAttribute())
                            {
                                attributes += string.Format(" {0}=\"{1}\"", reader.Name, reader.ReadContentAsString());
                            }
                            xmlContent = string.Format("<{0}{1}>\n", xmlHeaderName, attributes);
                            reader.MoveToElement();
                        }
                        xmlContent += reader.ReadInnerXml();
                        break;
                    }
                }
            }

            if (xmlContent == null) return;

            xmlContent += string.Format("</{0}>", xmlHeaderName);
            codeFiles.Add(new CodeFile()
            {
                Content = xmlContent,
                FileName = project.Name + ".xml"
            });
        }

        public static void DeployAddOns(string path, IEnumerable<IDeployableAddOn> addOns)
        {
            foreach (var addon in addOns)
            {
                addon.DeployAddOn(path);
            }
        }
    }
}