namespace CsLuaConverter
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    using ProjectInfo = CsLuaConverter.ProjectAnalysis.ProjectInfo;

    internal interface ISyntaxAnalyser
    {
        IEnumerable<Namespace> GetNamespaces(ProjectInfo projectInfo);
        IEnumerable<Namespace> GetNamespaces(Project project);
    }
}