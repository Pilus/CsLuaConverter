namespace CsLuaConverter
{
    using ProjectAnalysis;

    internal interface ISyntaxAnalyser
    {
        AnalyzedProjectInfo AnalyzeProject(ProjectInfo projectInfo);
    }
}