namespace GrindOMeter.View.Xml
{
    using BlizzardApi.WidgetInterfaces;
    using CsLuaFramework.Attributes;

    [ProvideSelf]
    public interface IGrindOMeterTrackingRow : IFrame
    {
        ITexture IconTexture { get; }
        IFontString Name { get; }
        IFontString Amount { get; }
        IFontString Velocity { get; }
        IButton ResetButton { get; }
        IButton RemoveButton { get; }
        IEntityId Id { get; set; }
    }
}
