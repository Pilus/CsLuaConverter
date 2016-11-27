
namespace CsLuaConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using ProjectAnalysis;

    public class AnalyzedProjectInfo
    {
        public AnalyzedProjectInfo()
        {
            this.HostOf = new List<AnalyzedProjectInfo>();
            this.RefersTo = new List<AnalyzedProjectInfo>();
        }

        public ProjectInfo Info { get; set; }
        public Namespace[] Namespaces { get; set; }
        public List<AnalyzedProjectInfo> Referenders { get; private set; }
        public List<AnalyzedProjectInfo> RequiredAddOns { get; private set; }
        public List<AnalyzedProjectInfo> HostOf { get; }
        public List<AnalyzedProjectInfo> RefersTo { get; private set; }

        public void PopulateReferences(List<AnalyzedProjectInfo> projects)
        {
            this.Referenders = projects
                .Where(project => project.Info.ReferencedProjects.Any(reff => reff.Equals(this.Info.Name)))
                .ToList();
            this.RefersTo =
                this.Info.ReferencedProjects.Select(name => projects.FirstOrDefault(p => p.Info.Name.Equals(name)))
                    .Where(v => v != null)
                    .ToList();

            if (this.Info.CsLuaAddOnAttribute != null && this.Info.CsLuaAddOnAttribute.Dependencies != null)
            {
                this.RequiredAddOns = this.Info.CsLuaAddOnAttribute.Dependencies
                    .Select(dep => projects.FirstOrDefault(p => p.Info.Name.Equals(dep)))
                    .Where(project => project != null)
                    .ToList();
            }
        }

        public void SetHosts(List<AnalyzedProjectInfo> hosts)
        {
            hosts.ForEach(host => host.HostOf.Add(this));
        }
    }
}
