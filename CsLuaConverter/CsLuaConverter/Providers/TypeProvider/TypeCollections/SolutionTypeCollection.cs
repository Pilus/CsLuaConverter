namespace CsLuaConverter.Providers.TypeProvider.TypeCollections
{
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Microsoft.CodeAnalysis;

    public class SolutionTypeCollection : BaseTypeCollection
    {
        public SolutionTypeCollection(Solution solution)
        {
            this.LoadSolution(solution);
        }


        private void LoadSolution(Solution solution)
        {
            foreach (var project in solution.Projects)
            {
                try
                {
                    this.LoadAssembly(Assembly.LoadFrom(project.OutputFilePath));
                }
                catch (FileNotFoundException)
                {
                    throw new ConverterException(string.Format("Could not find the file {0}. Please build or rebuild the {1} project.", project.OutputFilePath, project.Name));
                }
            }
        }

        private void LoadAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => !t.Name.StartsWith("<")))
            {
                this.Add(type);
            }
        }
    }
}