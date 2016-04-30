namespace CsLuaConverter
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    public class Converter
    {
        public async Task ConvertAsync(string solutionPath, string wowPath)
        {
            Console.WriteLine("Started CsToLua converter.");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var solution = await this.GetSolutionAsync(solutionPath);
            var providers = new Providers.Providers(solution);

            ISyntaxAnalyser analyzer = new Analyzer(providers);

            var solutionHandler = new SolutionHandler(analyzer);
            var addOns = solutionHandler.GenerateAddOnsFromSolution(solution);

            foreach (var addon in addOns)
            {
                addon.DeployAddOn(wowPath);
            }

            stopWatch.Stop();

            Console.WriteLine("Lua converting successfull. Time: {0}.{1} sec.", stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);
        }

        private async Task<Solution> GetSolutionAsync(string path)
        {
            var solutionFile = new FileInfo(path);
            if (!solutionFile.Exists)
            {
                throw new ConverterException(string.Format("Could not load the solution file: {0}", solutionFile.FullName));
            }

            MSBuildWorkspace workspace = null;
            try
            {
                workspace = MSBuildWorkspace.Create();
            }
            catch (ReflectionTypeLoadException ex)
            {
                var loaderExceptions = string.Join(", ", ex.LoaderExceptions.Select(e => e.Message).Distinct());
                throw new ConverterException($"ReflectionTypeLoadException happened during loading of solution: {loaderExceptions}.");
            }

            var solution = await workspace.OpenSolutionAsync(path);

            if (!solution.Projects.Any())
            {
                throw new ConverterException($"Solution {solutionFile.Name} seems to contain no projects.");
            }


            return solution;
        }
    }
}