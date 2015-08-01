

namespace CsLuaConverter.ReferenceAnalysis
{
    using CsLuaConverter.SyntaxAnalysis;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    static class ReferenceAnalyzer
    {
        public static void PopulateAndAnalyseReferences(List<AnalyzedProjectInfo> projects)
        {
            projects.ForEach(p => p.PopulateReferences(projects));
            projects.ForEach(AnalyseReferences);
        }

        private static void AnalyseReferences(AnalyzedProjectInfo project)
        {
            if (project.Info.IsLibrary())
            {
                AnalyseLibraryReferences(project);
            }
        }

        private static void AnalyseLibraryReferences(AnalyzedProjectInfo project)
        {
            var nodes = new List<HostGraphNode>();
            CreateHostGraph(project, nodes);
            RemoveAddOnsThatAreNotNearestHostCandidates(nodes);
            project.SetHosts(nodes.Where(FittingAddOnHost)
                .Select(hostNode => hostNode.ProjectInfo).ToList());
        }

        private static HostGraphNode CreateHostGraph(AnalyzedProjectInfo project, List<HostGraphNode> nodes)
        {
            var node = new HostGraphNode(project);
            nodes.Add(node);

            (project.Referenders ?? project.RequiredAddOns).ForEach(reqProj =>
                {
                    var reqNode = nodes.FirstOrDefault(n => n.ProjectInfo.Equals(reqProj)) ??
                               CreateHostGraph(reqProj, nodes);
                    reqNode.Parents.Add(node);
                    node.Children.Add(reqNode);
                });
            
            return node;
        }

        private static void RemoveAddOnsThatAreNotNearestHostCandidates(List<HostGraphNode> nodes)
        {
            while (nodes.Any(AddOnWithNoChildrenAndNoParentLibraries))
            {
                var node = nodes.First(AddOnWithNoChildrenAndNoParentLibraries);
                nodes.Remove(node);
                node.Parents.ForEach(p => p.Children.Remove(node));
                node.Parents.Clear();
            }
        }

        private static bool AddOnWithNoChildrenAndNoParentLibraries(HostGraphNode node)
        {
            return node.Type.Equals(HostGraphNodeType.AddOn) && 
                node.Children.Count == 0 && 
                node.Parents.Count(p => p.Type.Equals(HostGraphNodeType.AddOn)).Equals(1);
        }

        private static bool FittingAddOnHost(HostGraphNode node)
        {
            return node.Type.Equals(HostGraphNodeType.AddOn) && node.Children.Count == 0;
        }
    }
}
