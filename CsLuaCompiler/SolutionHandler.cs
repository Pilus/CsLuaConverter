namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using ProjectAnalysis;
    using ProjectInfo = ProjectAnalysis.ProjectInfo;
    using CsLuaCompiler.SyntaxAnalysis;

    internal static class SolutionHandler
    {

        private static ProjectInfo GetProjectByName(this IEnumerable<ProjectInfo> projects, string name)
        {
            return projects.FirstOrDefault(project => project.Name.Equals(name));
        }

        private static IEnumerable<ProjectInfo> GetRootProjects(IEnumerable<ProjectInfo> projects)
        {
            return projects.Where(project =>
                project.IsAddOn() &&
                !project.ReferencedProjects.Any(referenceName =>
                {
                    var p = GetProjectByName(projects, referenceName);
                    return p != null && p.IsAddOn();
                }));
        }

        private static IList<ProjectInfo> GetReferenders(this ProjectInfo project, IEnumerable<ProjectInfo> projects)
        {
            return projects.Where(p => p.ReferencedProjects.Contains(project.Name)).ToList();
        }

        private static void AddAddOnAndReferencesToList(ProjectInfo project, IList<ProjectInfo> nonDeployedProjects, IList<IDeployableAddOn> addOns, List<CodeFile> additionalFiles, IProviders providers)
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
            

            foreach (var refName in project.ReferencedProjects)
            {
                var refProject = nonDeployedProjects.GetProjectByName(refName);
                if (refProject == null)
                {
                    continue;
                }
                
                switch (refProject.ProjectType)
                {
                    case ProjectType.CsLuaLibrary:
                        luaFiles.AddRange(LuaFileWriter.GetLuaFiles(CsProject.GetStructuredSyntaxTree(refProject.ProjectType, refProject.Project), providers, refProject.Name, refProject.RequiresCsLuaMetaHeader, refProject.ProjectPath));
                        AddXmlToCodeFiles(refProject, luaFiles);
                        break;
                    case ProjectType.LuaLibrary:
                        luaFiles.AddRange(LuaFileWriter.GetLuaFiles(CsProject.GetStructuredSyntaxTree(refProject.ProjectType, refProject.Project), providers, refProject.Name, refProject.RequiresCsLuaMetaHeader, refProject.ProjectPath));
                        AddXmlToCodeFiles(refProject, luaFiles);
                        break;
                    default:
                        throw new System.Exception("Unknown project type.");
                }

                nonDeployedProjects.Remove(refProject);
            }

            luaFiles.AddRange(LuaFileWriter.GetLuaFiles(CsProject.GetStructuredSyntaxTree(project.ProjectType, project.Project), providers, project.Name, project.RequiresCsLuaMetaHeader, project.ProjectPath));
            AddXmlToCodeFiles(project, luaFiles);
            var resources = GetResourceFiles(project.Project);

            foreach (var referender in project.GetReferenders(nonDeployedProjects))
            {
                switch (referender.ProjectType)
                {
                    case ProjectType.CsLuaAddOn:
                        AddAddOnAndReferencesToList(referender, nonDeployedProjects, addOns, null, providers);
                        break;
                    case ProjectType.LuaAddOn:
                        if (!addOns.Any(addon => addon.Name.Equals(referender.Name)))
                        {
                            addOns.Add(new LuaAddOn(referender.Name, referender.ProjectPath));
                        }

                        break;
                    case ProjectType.LuaLibrary:
                    case ProjectType.CsLuaLibrary:
                    case ProjectType.Ignored:
                        break;
                    default:
                        throw new System.Exception("Unknown project type.");
                }
            }

            if (project.ProjectType.Equals(ProjectType.CsLuaAddOn))
            {
                addOns.Add(new AddOn(luaFiles, resources, project.CsLuaAddOnAttribute));
            }
            else
            {
                addOns.Add(new LuaAddOn(project.Name, project.ProjectPath));
            }
            
        }

        public static IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution, IProviders providers)
        {
            var projects = solution.Projects.Select(project => ProjectAnalyser.AnalyzeProject(project))
                .Where(project => !project.ProjectType.Equals(ProjectType.Ignored))
                .ToList();

            var syntaxAnalyser = new SyntaxAnalyser();
            var analyzedProjects = projects.Select(project => syntaxAnalyser.AnalyzeProject(project));



            var addOns = new List<IDeployableAddOn>();

            var rootAddOnProjects = GetRootProjects(projects);
            foreach (var rootProject in rootAddOnProjects.ToList())
            {
                AddAddOnAndReferencesToList(rootProject, projects, addOns, new List<CodeFile>() { CsLuaMetaReader.GetMetaFile() }, providers);
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

        private static void AddXmlToCodeFiles(ProjectInfo project, IList<CodeFile> codeFiles)
        {
            var projectDir = new FileInfo(project.Project.FilePath).Directory;
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
    }
}