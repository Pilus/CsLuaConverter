namespace CsLuaCompiler
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;
    using Providers;

    internal class Program
    {
        private static int Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                Convert(args[0], args[1]);
                return 0;
            }
            else
            { 
                Console.WriteLine("Started CsToLua converter.");
                try
                {
                    Convert(args[0], args[1]);
                    return 0;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", exception.Message));
                    return -1;
                }
            }
        }

        private static void Convert(string solutionPath, string wowPath)
        {
            var solution = GetSolution(solutionPath);
            var providers = new Providers.Providers(solution);
            var addOns = SolutionHandler.GenerateAddOnsFromSolution(solution, providers);

            foreach (var addon in addOns)
            {
                addon.DeployAddOn(wowPath);
            }

            Console.WriteLine("Lua converting successfull.");
        }

        private static Solution GetSolution(string path)
        {
            var solutionFile = new FileInfo(path);
            if (!solutionFile.Exists)
            {
                throw new CompilerException(string.Format("Could not load the solution file: {0}", solutionFile.FullName));
            }

            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Task<Solution> loadSolution = workspace.OpenSolutionAsync(path);
            loadSolution.Wait();
            return loadSolution.Result;
        }
    }
}