namespace CsLuaConverter
{
    using System.Collections.Generic;

    using ProjectAnalysis;

    internal interface ISyntaxAnalyser
    {
        IEnumerable<Namespace> AnalyzeProject(ProjectInfo projectInfo);
    }
}