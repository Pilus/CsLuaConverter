
namespace CsLuaCompiler.ReferenceAnalysis
{
    using CsLuaCompiler.SyntaxAnalysis;

    class HostGraphNode
    {
        public HostGraphNode(AnalyzedProjectInfo projectInfo)
        {
            this.ProjectInfo = projectInfo;
            this.Type = projectInfo.Info.IsAddOn() ? HostGraphNodeType.AddOn : HostGraphNodeType.Library;
        }

        public AnalyzedProjectInfo ProjectInfo { get; private set; }
        public HostGraphNode[] Children { get; set; }
        public HostGraphNode[] Parents { get; set; }
        public HostGraphNodeType Type { get; private set; }
    }
}
