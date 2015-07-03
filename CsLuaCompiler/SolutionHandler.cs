namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.Linq;
    using AddOnConstruction;
    using Microsoft.CodeAnalysis;
    using ProjectAnalysis;
    using Providers;
    using ReferenceAnalysis;
    using SyntaxAnalysis;

    internal static class SolutionHandler
    {
        public static IEnumerable<IDeployableAddOn> GenerateAddOnsFromSolution(Solution solution, IProviders providers)
        {
            var projects = solution.Projects.Select(project => ProjectAnalyser.AnalyzeProject(project))
                .Where(project => !project.ProjectType.Equals(ProjectType.Ignored))
                .ToList();

            var syntaxAnalyser = new SyntaxAnalyser();
            var analyzedProjects = projects.Select(project => syntaxAnalyser.AnalyzeProject(project)).ToList();

            ReferenceAnalyzer.PopulateAndAnalyseReferences(analyzedProjects);

            var structurer = new AddOnConstructor(providers);
            return structurer.StructureAddOns(analyzedProjects);
        }

    }
}