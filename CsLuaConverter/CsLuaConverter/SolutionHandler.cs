namespace CsLuaConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using AddOnConstruction;
    using Microsoft.CodeAnalysis;
    using ProjectAnalysis;
    using ReferenceAnalysis;

    internal class SolutionHandler
    {
        private readonly NamespaceConstructor namespaceConstructor;

        public SolutionHandler(NamespaceConstructor namespaceConstructor)
        {
            this.namespaceConstructor = namespaceConstructor;
        }

        public IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution)
        {
            var projects = solution.Projects.Select(ProjectAnalyser.AnalyzeProject)
                .Where(project => !project.ProjectType.Equals(ProjectType.Ignored))
                .ToList();

            var analyzedProjects =
                projects.Select(
                    project =>
                    new AnalyzedProjectInfo()
                        {
                            Info = project,
                            Namespaces = this.GetNamespaces(project)
                        }).ToList();

            ReferenceAnalyzer.PopulateAndAnalyseReferences(analyzedProjects);

            var structurer = new AddOnConstructor();
            return structurer.StructureAddOns(analyzedProjects);
        }

        private Namespace[] GetNamespaces(ProjectAnalysis.ProjectInfo projectInfo)
        {
            if (!projectInfo.IsCsLua())
            {
                return null;
            }

            return this.namespaceConstructor.GetNamespacesFromProject(projectInfo.Project).ToArray();
        }

    }
}