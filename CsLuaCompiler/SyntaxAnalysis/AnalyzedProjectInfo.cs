
namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.Collections.Generic;
    using System.Linq;
    using ProjectAnalysis;

    class AnalyzedProjectInfo
    {
        public ProjectInfo Info { get; set; }
        public Dictionary<string, NameSpace> Namespaces { get; set; }
        public List<AnalyzedProjectInfo> Referenders { get; private set; }

        public void PopulateReferences(List<AnalyzedProjectInfo> projects)
        {
            this.Referenders = projects
                .Where(project => project.Info.ReferencedProjects.Any(reff => reff.Equals(this.Info.Name)))
                .ToList();
        }
    }
}
