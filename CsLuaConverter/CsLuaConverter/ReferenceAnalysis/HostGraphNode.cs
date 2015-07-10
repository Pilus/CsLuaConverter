
namespace CsLuaConverter.ReferenceAnalysis
{
    using System.Collections.Generic;
    using CsLuaConverter.SyntaxAnalysis;

    class HostGraphNode
    {
        public HostGraphNode(AnalyzedProjectInfo projectInfo)
        {
            this.ProjectInfo = projectInfo;
            this.Type = projectInfo.Info.IsAddOn() ? HostGraphNodeType.AddOn : HostGraphNodeType.Library;
            this.Children = new List<HostGraphNode>();
            this.Parents = new List<HostGraphNode>();
        }

        public AnalyzedProjectInfo ProjectInfo { get; private set; }
        public List<HostGraphNode> Children { get; set; }
        public List<HostGraphNode> Parents { get; set; }
        public HostGraphNodeType Type { get; private set; }
    }
}
