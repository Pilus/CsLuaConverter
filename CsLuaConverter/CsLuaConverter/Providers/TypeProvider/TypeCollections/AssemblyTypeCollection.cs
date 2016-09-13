namespace CsLuaConverter.Providers.TypeProvider.TypeCollections
{
    using System.Linq;
    using System.Reflection;

    public class AssemblyTypeCollection : BaseTypeCollection
    {
        public AssemblyTypeCollection(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => !t.Name.StartsWith("<")))
            {
                this.Add(type);
            }
        }
    }
}