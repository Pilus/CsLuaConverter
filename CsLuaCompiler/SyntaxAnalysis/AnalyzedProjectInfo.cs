
namespace CsLuaCompiler.SyntaxAnalysis
{
    using CsLuaCompiler.SolutionAnalysis;
    using System.Collections.Generic;

    class AnalyzedProjectInfo
    {
        public ProjectInfo Info { get; set; }
        public Dictionary<string, NameSpace> Namespaces { get; set; }
    }
}
