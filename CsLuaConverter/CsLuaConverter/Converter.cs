namespace CsLuaConverter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CsLuaConverter.CodeTreeLuaVisitor;
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

            this.ConvertSolution(solution, wowPath);

            stopWatch.Stop();

            Console.WriteLine("Lua converting successfull. Time: {0}.{1} sec.", stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);
        }

        public void Convert(string solutionPath, string wowPath)
        {
            Console.WriteLine("Started CsToLua converter.");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var solutionTask = this.GetSolutionAsync(solutionPath);
            solutionTask.Wait();

            this.ConvertSolution(solutionTask.Result, wowPath);

            stopWatch.Stop();

            Console.WriteLine("Lua converting successfull. Time: {0}.{1} sec.", stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);
        }


        private void ConvertSolution(Solution solution, string wowPath)
        {
            /*
            var typeCollections = new List<BaseTypeCollection>
            {
                new ManuallyImplementedSystemTypeTypeCollection(), 
                new AssemblyTypeCollection(Assembly.Load("Lua")), 
                new AssemblyTypeCollection(Assembly.Load("CsLuaFramework")), 
                new SolutionTypeCollection(solution), 
            };

            typeCollections.AddRange(TypesFileTypeCollection.LoadFromCurrentDir()); */

            var providers = new Providers.Context();

            var treeVisitor = new CodeTreeVisitor(providers);

            ISyntaxAnalyser analyzer = new Analyzer(treeVisitor);

            var solutionHandler = new SolutionHandler(analyzer);
            var addOns = solutionHandler.GenerateAddOnsFromSolution(solution);

            foreach (var addon in addOns)
            {
                addon.DeployAddOn(wowPath);
            }
        }

        private async Task<Solution> GetSolutionAsync(string path)
        {
            var solutionFile = new FileInfo(path);
            if (!solutionFile.Exists)
            {
                throw new ConverterException($"Could not load the solution file: {solutionFile.FullName}");
            }

            MSBuildWorkspace workspace;
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