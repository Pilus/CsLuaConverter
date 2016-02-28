namespace MyAddOn
{
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using Lua;

    [CsLuaAddOn("MyAddOn", "My AddOn", 60200)]
    public class MyAddOn : ICsLuaAddOn
    {
        public void Execute()
        {
            Core.print("My AddOn loaded.");
        }
    }
}