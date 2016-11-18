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
        private readonly ISyntaxAnalyser syntaxAnalyzer;

        public SolutionHandler(ISyntaxAnalyser syntaxAnalyzer)
        {
            this.syntaxAnalyzer = syntaxAnalyzer;
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
                            Namespaces = this.syntaxAnalyzer.GetNamespaces(project).ToArray()
                        }).ToList();

            ReferenceAnalyzer.PopulateAndAnalyseReferences(analyzedProjects);

            var structurer = new AddOnConstructor();
            return structurer.StructureAddOns(analyzedProjects);
        }

    }
}