namespace CsLuaConverter.AddOnConstruction
{
    public interface IDeployableAddOn
    {
        string Name { get; }
        void DeployAddOn(string path);
    }
}