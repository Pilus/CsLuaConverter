namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsLuaAttributes;

    internal class AddOn : IDeployableAddOn
    {
        private readonly IList<CodeFile> codeFiles;
        private readonly string name;
        private readonly CodeFile tocFile;
        private readonly IEnumerable<ResourceFile> resourceFiles;

        public AddOn(IList<CodeFile> codeFiles, IEnumerable<ResourceFile> resourceFiles, CsLuaAddOnAttribute attribute)
        {
            this.name = attribute.Name;
            this.codeFiles = codeFiles;
            this.resourceFiles = resourceFiles;

            var tocBuilder = new TocBuilder(codeFiles, attribute);
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
    }
}