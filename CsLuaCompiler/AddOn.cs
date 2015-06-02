namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class AddOn : IDeployableAddOn
    {
        private readonly IList<CodeFile> luaFiles;
        private readonly string name;
        private readonly IDictionary<string, object> settings;
        private readonly CodeFile tocFile;
        private readonly IEnumerable<ResourceFile> resourceFiles;

        public AddOn(string name, Dictionary<string, object> settings, IList<CodeFile> luaFiles, IEnumerable<ResourceFile> resourceFiles)
        {
            this.name = name;
            this.settings = settings;
            this.luaFiles = luaFiles;
            this.resourceFiles = resourceFiles;
            this.tocFile = new CodeFile {FileName = this.name + ".toc", Content = this.GetTocFileContent()};
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
            foreach (CodeFile luaFile in this.luaFiles)
            {
                luaFile.DeployFile(addonPath);
            }

            foreach (var resourceFile in this.resourceFiles)
            {
                resourceFile.DeployFile(addonPath);
            }
        }

        private string GetTocFileContent()
        {
            var s = string.Empty;
            s += this.WriteTags(this.settings.Keys.Where(key => !key.StartsWith("x-")));
            s += this.WriteTags(this.settings.Keys.Where(key => key.StartsWith("x-")));
            s += string.Join("", this.luaFiles.Select(file => file.FileName + "\n"));
            s += string.Join("", this.resourceFiles.Where(res => res.FullPath.EndsWith(".xml")).Select(res => res.RelativePath + "\n"));
            return s;
        }

        private string WriteTags(IEnumerable<string> tags)
        {
            return tags.Aggregate(string.Empty,
                (current, tag) => current + string.Format("## {0}: {1}\n", tag, this.settings[tag]));
        }
    }
}