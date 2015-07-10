
namespace CsLuaConverter.AddOnConstruction
{
    using System.IO;

    class ResourceFile
    {
        public string FullPath { get; set; }
        public string RelativePath { get; set; }

        public void DeployFile(string targetPath)
        {
            var destination = Path.Combine(targetPath, this.RelativePath);
            var destDir = Path.GetDirectoryName(destination);
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            File.Copy(this.FullPath, destination, true);
        }
    }
}
