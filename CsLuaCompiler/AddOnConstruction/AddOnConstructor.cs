namespace CsLuaCompiler.AddOnConstruction
{
    using System.Collections.Generic;
    using System.Linq;
    using Providers;
    using SyntaxAnalysis;

    class AddOnConstructor
    {
        private readonly IProviders providers;

        public AddOnConstructor(IProviders providers)
        {
            this.providers = providers;
        }

        public IEnumerable<IDeployableAddOn> StructureAddOns(IEnumerable<AnalyzedProjectInfo> projects)
        {

            return projects.Select(StructureAddOn).Where(a => a != null);
        }

        public IDeployableAddOn StructureAddOn(AnalyzedProjectInfo project)
        {
            switch (project.Info.ProjectType)
            {
                case ProjectType.CsLuaAddOn:
                    return new AddOn(project, this.providers);
                case ProjectType.LuaAddOn:
                    return new LuaAddOn(project.Info.Name, project.Info.ProjectPath);
                default:
                    return null;
            }
        }
    }
}
