namespace CsToLua
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;

    internal static class SolutionHandler
    {
        public static IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution, FullNameProvider nameProvider)
        {
            List<CsProject> csProjects =
                solution.Projects.Select(project => new CsProject(nameProvider, project)).ToList();

            var addOns = new List<IDeployableAddOn>();
            while (true)
            {
                CsProject mainProj = csProjects.FirstOrDefault(
                    pj => pj.IsCsLuaAddOn() &&
                    pj.GetReferences()
                        .Select(name =>
                        {
                            CsProject proj =
                                csProjects.FirstOrDefault(
                                    csProj => csProj.Name.Equals(name));
                            return proj != null && proj.IsCsLuaAddOn();
                        })
                        .All(addon => addon != true));

                if (mainProj == null)
                {
                    break;
                }

                csProjects.Remove(mainProj);

                List<CodeFile> luaFiles = mainProj.GetLuaFiles().ToList();

                IEnumerable<CsProject> refProjects = mainProj.GetReferences()
                    .Select(name => csProjects.FirstOrDefault(proj => proj.Name.Equals(name)))
                    .Where(proj => proj != null);

                foreach (CsProject refProject in refProjects)
                {
                    List<CodeFile> currentFiles = luaFiles;
                    luaFiles = refProject.GetLuaFiles().ToList();
                    luaFiles.AddRange(currentFiles);
                    csProjects.Remove(refProject);
                }

                var resources = GetResourceFiles(mainProj.CodeProject).ToList();
                var xmlRes = GetXmlFiles(mainProj.CodeProject);
                if (xmlRes != null)
                {
                    luaFiles.Add(xmlRes);
                }

                addOns.Add(new AddOn(mainProj.Name, mainProj.Settings, luaFiles, resources));
            }

            foreach (var csProject in csProjects)
            {
                if (!csProject.IsCsLuaAddOn() && csProject.IsLuaAddOn())
                {
                    addOns.Add(new LuaAddOn(csProject.Name,csProject.GetProjectPath()));
                }
            }

            return addOns;
        }


        private static readonly string[] ResourceFileExtensions = { "*.tga", "*.ttf", "*.blp", "*.mp3", "*.wav", "*.ogg", "*.txt", "*.xml" };

        private static IEnumerable<ResourceFile> GetResourceFiles(Project project)
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
                });
        }

        private static CodeFile GetXmlFiles(Project project)
        {
            var projectDir = new FileInfo(project.FilePath).Directory;
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

            if (xmlContent == null) return null;

            xmlContent += string.Format("</{0}>", xmlHeaderName);
            return new CodeFile()
            {
                Content = xmlContent,
                FileName = project.Name + ".xml"
            };
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