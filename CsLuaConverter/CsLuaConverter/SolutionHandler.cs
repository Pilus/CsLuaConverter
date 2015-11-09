namespace CsLuaConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using AddOnConstruction;
    using Microsoft.CodeAnalysis;
    using ProjectAnalysis;
    using Providers;
    using ReferenceAnalysis;
    using SyntaxAnalysis;

    internal class SolutionHandler
    {
        private readonly ISyntaxAnalyser syntaxAnalyzer;

        public SolutionHandler(ISyntaxAnalyser syntaxAnalyzer)
        {
            this.syntaxAnalyzer = syntaxAnalyzer;
        }

        public IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution, IProviders providers)
        {
            var projects = solution.Projects.Select(ProjectAnalyser.AnalyzeProject)
                .Where(project => !project.ProjectType.Equals(ProjectType.Ignored))
                .ToList();

            var analyzedProjects = projects.Select(project => this.syntaxAnalyzer.AnalyzeProject(project)).ToList();

            ReferenceAnalyzer.PopulateAndAnalyseReferences(analyzedProjects);

            var structurer = new AddOnConstructor(providers);
            return structurer.StructureAddOns(analyzedProjects);
        }

    }
}