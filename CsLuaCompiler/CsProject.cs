namespace CsLuaCompiler
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using SyntaxAnalysis;
    using CsLuaAttributes;
    using System.Reflection;

    internal class CsProject
    {
        
        public static Dictionary<string, NameSpace> GetStructuredSyntaxTree(ProjectType type, Project project)
        {
            if (type.Equals(ProjectType.CsLuaAddOn) || type.Equals(ProjectType.CsLuaLibrary))
            {
                if (Debugger.IsAttached)
                {
                    return GetNameSpaces(project);
                }
                
                try
                {
                    return GetNameSpaces(project);
                }
                catch (Exception ex)
                {

                    throw new WrappingException(string.Format("In project: {0}.", project.Name), ex);
                }
            }
            return null;
        }


        private static Dictionary<string, NameSpace> GetNameSpaces(Project project)
        {
            IEnumerable<Document> docs = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var nameSpaces = new Dictionary<string, NameSpace>();
            foreach (Document document in docs)
            {
                NameSpacePart nameSpacePart = new SyntaxAnalyser().AnalyseDocument(document);
                if (nameSpaces.ContainsKey(nameSpacePart.FullName.First()))
                {
                    nameSpaces[nameSpacePart.FullName.First()].AddPart(nameSpacePart);
                }
                else
                {
                    nameSpaces[nameSpacePart.FullName.First()] = new NameSpace(nameSpacePart, 1);
                }
            }
            return nameSpaces;
        }
    }
}