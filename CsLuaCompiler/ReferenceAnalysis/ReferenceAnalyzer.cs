

namespace CsLuaCompiler.ReferenceAnalysis
{
    using CsLuaCompiler.SyntaxAnalysis;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ReferenceAnalyzer
    {
        public static void PopulateAndAnalyseReferences(List<AnalyzedProjectInfo> projects)
        {
            foreach (var project in projects)
            {
                project.PopulateReferences(projects);
                AnalyseReferences(project);
            }
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
        }

        private static HostGraphNode CreateHostGraph(AnalyzedProjectInfo project, List<HostGraphNode> nodes)
        {
            var node = new HostGraphNode(project);
            if (project.Info.IsAddOn())
            {
                
            }
            else if (project.Info.IsLibrary())
            {
                //project.Referenders
            }
            else
            {
                return null;
            }
            return node;
        }
    }
}
