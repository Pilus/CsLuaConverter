
namespace CsLuaConverter
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using ProjectAnalysis;
    using Providers;

    public class AnalyzedProjectInfo
    {
        public AnalyzedProjectInfo()
        {
            this.HostOf = new List<AnalyzedProjectInfo>();
        }

        public ProjectInfo Info { get; set; }
        public Dictionary<string, Action<IndentedTextWriter, IProviders>> Namespaces { get; set; }
        public List<AnalyzedProjectInfo> Referenders { get; private set; }
        public List<AnalyzedProjectInfo> RequiredAddOns { get; private set; }
        public List<AnalyzedProjectInfo> HostOf { get; private set; }

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

        public void SetHosts(List<AnalyzedProjectInfo> hosts)
        {
            hosts.ForEach(host => host.HostOf.Add(this));
        }
    }
}
