namespace CsLuaConverter.AddOnConstruction
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using CsLuaFramework.Attributes;

    internal class AddOn : IDeployableAddOn
    {
        private static readonly string[] ResourceFileExtensions = { "*.tga", "*.ttf", "*.blp", "*.mp3", "*.wav", "*.ogg", "*.txt", "*.xml" };

        private readonly List<CodeFile> codeFiles;
        private readonly string name;
        private readonly CodeFile tocFile;
        private readonly IEnumerable<ResourceFile> resourceFiles;


        public AddOn(AnalyzedProjectInfo projectInfo)
        {
            this.name = projectInfo.Info.Name;

            this.codeFiles = new List<CodeFile>();
            if (!projectInfo.RefersTo.Any(pi => pi.Info.ProjectType.Equals(ProjectType.CsLuaAddOn)))
            {
                this.codeFiles.Add(CsLuaMetaReader.GetMetaFile());
            }

            var projectPath = projectInfo.Info.ProjectPath;

            foreach (var hostedProject in projectInfo.HostOf)
            {
                this.codeFiles.AddRange(LuaFileWriter.GetLuaFiles(hostedProject.Namespaces, hostedProject.Info.Name, false, hostedProject.Info.ProjectPath));
            }

            var xmlFile = GetXmlCodeFile(projectPath, this.name);
            if (xmlFile != null)
            {
                this.codeFiles.Add(xmlFile);
            }

            this.codeFiles.AddRange(LuaFileWriter.GetLuaFiles(projectInfo.Namespaces, this.Name, false, projectPath));
            
            this.resourceFiles = GetResourceFiles(projectPath);

            var tocBuilder = new TocBuilder(this.codeFiles, projectInfo.Info.CsLuaAddOnAttribute);
            this.tocFile = new CodeFile { FileName = this.name + ".toc", Content = tocBuilder.Build() };
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public void DeployAddOn(string path)
        {
            var addonPath = Path.Combine(path, this.name);

            if (!Directory.Exists(addonPath))
            {
                Directory.CreateDirectory(addonPath);
            }

            this.tocFile.DeployFile(addonPath);
            foreach (var luaFile in this.codeFiles)
            {
                luaFile.DeployFile(addonPath);
            }

            foreach (var resourceFile in this.resourceFiles.Where(res => !res.FullPath.EndsWith(".xml")))
            {
                resourceFile.DeployFile(addonPath);
            }
        }

        private static IList<ResourceFile> GetResourceFiles(string projectFilePath)
        {
            var projectDir = new DirectoryInfo(projectFilePath);
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


        private static CodeFile GetXmlCodeFile(string projectFilePath, string projectName)
        {
            var projectDir = new DirectoryInfo(projectFilePath);
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
                FileName = projectName + ".xml"
            };
        }
    }
}