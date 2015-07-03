
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
        public List<AnalyzedProjectInfo> RequiredAddOns { get; private set; }
        public List<AnalyzedProjectInfo> Hosts { get; set; }

        public void PopulateReferences(List<AnalyzedProjectInfo> projects)
        {
            this.Referenders = projects
                .Where(project => project.Info.ReferencedProjects.Any(reff => reff.Equals(this.Info.Name)))
                .ToList();
            if (this.Info.CsLuaAddOnAttribute != null && this.Info.CsLuaAddOnAttribute.Dependencies != null)
            {
                this.RequiredAddOns = this.Info.CsLuaAddOnAttribute.Dependencies
                    .Select(dep => projects.FirstOrDefault(p => p.Info.Name.Equals(dep)))
                    .Where(project => project != null)
                    .ToList();
            }
        }
    }
}
