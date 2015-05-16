﻿namespace CsToLua
{
    using System.IO;
    using System.Linq;

    public class LuaAddOn : IDeployableAddOn
    {
        private readonly string addonPath;
        private readonly string addonName;

        private readonly string[] extensions =
            new[] {"*.lua", "*.toc", "*.tga", "*.ttf", "*.blp", "*.mp3", "*.wav", "*.ogg", "*.txt", "*.xml"};

        public LuaAddOn(string addonName, string addonPath)
        {
            this.addonName = addonName;
            this.addonPath = addonPath;
        }


        public void DeployAddOn(string path)
        {
            var targetPath = path + "\\" + this.addonName;

            foreach (string dirPath in Directory.GetDirectories(addonPath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(addonPath, targetPath));

            foreach (var ext in this.extensions)
            {
                foreach (var newPath in Directory.GetFiles(this.addonPath, ext,
                    SearchOption.AllDirectories))
                { 
                    File.Copy(newPath, newPath.Replace(this.addonPath, targetPath), true);
                }
            }
        }
    }
}