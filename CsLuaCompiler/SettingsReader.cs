namespace CsToLua
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.CodeAnalysis;

    internal static class SettingsReader
    {
        public static Dictionary<string, object> GetSettings(Project project)
        {
            string path = GetSettingsPath(project);
            if (path != null)
            {
                return ReadSettings(path);
            }

            return new Dictionary<string, object>();
        }

        private static string GetSettingsPath(Project project)
        {
            Document settingsDocument =
                project.Documents.Where(doc => doc.FilePath.EndsWith("Settings.Designer.cs")).FirstOrDefault();

            if (settingsDocument != null)
            {
                string path = settingsDocument.FilePath.Replace(settingsDocument.Name, "Settings.settings");
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }

        private static Dictionary<string, object> ReadSettings(string settingsFilePath)
        {
            var settings = new Dictionary<string, object>();
            using (XmlReader reader = XmlReader.Create(new StreamReader(settingsFilePath)))
            {
                while (reader.Read())
                {
                    if (reader.Name.Equals("Setting"))
                    {
                        string name = reader["Name"];
                        string type = reader["Type"];

                        if (name != null && type != null)
                        {
                            while (!reader.Name.Equals("Value"))
                            {
                                reader.Read();
                            }
                            reader.Read();

                            TypeConverter converter = TypeDescriptor.GetConverter(Type.GetType(type));
                            settings.Add(name, converter.ConvertFrom(reader.Value));
                        }
                    }
                }
                reader.Close();
            }

            return settings;
        }
    }
}