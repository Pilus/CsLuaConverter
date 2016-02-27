namespace GrindOMeter.View.Xml
{
    using BlizzardApi.WidgetInterfaces;
    using CsLuaFramework.Attributes;

    [ProvideSelf]
    public interface IGrindOMeterFrame : IFrame
    {
        IFontString Label { get; }
        IButton TrackButton { get; }
        IFrame TrackingContainer { get; }
    }
}
