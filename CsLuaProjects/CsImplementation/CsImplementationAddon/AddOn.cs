namespace CsImplementationAddon
{
    using CsLuaFramework.Attributes;

    [CsLuaAddOn("CsImplementationAddon", "CsImplementationAddon", 70000, Author = "Pilus", Notes = "Dummy addon for the Cs Implemenation.")]
    public class AddOn
    {
        public AddOn()
        {
            var e = new SystemZZZ.SystemException(); // Dummy to generate usage of SystemZZZ.
        }
    }
}
