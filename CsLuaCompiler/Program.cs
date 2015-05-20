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
            var debugOutput = new DirectoryInfo("DebugOutput");
            if (debugOutput.Exists)
            {
                foreach (FileInfo file in debugOutput.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }

            Solution solution = GetSolution(solutionPath);
            //List<Assembly> assemblies = LoadAssembliesInSolution(solution);
            //var nameProvider = new FullNameProvider(assemblies);
            var providers = new Providers.Providers(solution);
            IEnumerable<IDeployableAddOn> addOns = SolutionHandler.GenerateAddOnsFromSolution(solution, providers);
            SolutionHandler.DeployAddOns(wowPath, addOns);

            Console.WriteLine("Lua converting successfull.");
        }

        private static List<Assembly> LoadAssembliesInSolution(Solution solution)
        {
            var list = new List<Assembly>();
            foreach (var project in solution.Projects)
            {
                var dir = new FileInfo(project.OutputFilePath);
                LoadAssembliesInSolution(dir.Directory, list);
            }

            return list;
        }

        private static void LoadAssembliesInSolution(DirectoryInfo dir, List<Assembly> list)
        {
            foreach (FileInfo file in dir.GetFiles("*.dll"))
            {
                AddNewAssemblyToList(list, Assembly.LoadFrom(file.FullName));
            }

            foreach (FileInfo file in dir.GetFiles("*.exe"))
            {
                AddNewAssemblyToList(list, Assembly.LoadFrom(file.FullName));
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                LoadAssembliesInSolution(subDir, list);
            }
        }

        private static void AddNewAssembliesToList(ICollection<Assembly> list, IEnumerable<Assembly> newAssemblies)
        {
            foreach (Assembly newAssembly in newAssemblies)
            {
                AddNewAssemblyToList(list, newAssembly);
            }
        }

        private static void AddNewAssemblyToList(ICollection<Assembly> list, Assembly assembly)
        {
            if (list.All(a => a.FullName != assembly.FullName))
            {
                list.Add(assembly);
            }
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