
namespace GrindOMeter
{
    using BlizzardApi.Global;
    using CsLuaFramework;
    using CsLuaFramework.Attributes;
    using CsLuaFramework.Wrapping;
    using Lua;
    using Model.EntityAdaptor;
    using Model.EntityStorage;
    using View;

    [CsLuaAddOn("GrindOMeter", "Grind-O-Meter", 60200, 
        Author = "Pilus",
        Notes = "Tracking system for items and currencies.",
        SavedVariablesPerCharacter = new []{ EntityStorage.TrackedEntitiesSavedVariableName })]
    public class GrindOMeterAddOn : ICsLuaAddOn
    {
        public void Execute()
        {
            var model = new Model.Model(new EntityAdaptorFactory(), new EntityStorage());
            var view = new View.View(new EntitySelectionDropdownHandler(), Global.Wrapper);
            var presenter = new Presenter.Presenter(model, view);

            Core.print("Grind-O-Meter loaded.");
        }
    }
}
