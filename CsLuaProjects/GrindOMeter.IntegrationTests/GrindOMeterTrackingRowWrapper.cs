namespace GrindOMeter.IntegrationTests
{
    using BlizzardApi.WidgetInterfaces;
    using GrindOMeter.View;
    using GrindOMeter.View.Xml;
    using WoWSimulator.UISimulation;
    using WoWSimulator.UISimulation.UiObjects;
    using WoWSimulator.UISimulation.XMLHandler;

    public class GrindOMeterTrackingRowWrapper : Frame, IGrindOMeterTrackingRow
    {
        public static IUIObject Init(UiInitUtil util, LayoutFrameType layout, IRegion parent)
        {
            return new GrindOMeterTrackingRowWrapper(util, layout, parent);
        }

        public GrindOMeterTrackingRowWrapper(UiInitUtil util, LayoutFrameType layout, IRegion parent)
            : base(util, "frame", layout as FrameType, parent)
        { }

        public ITexture IconTexture
        {
            get { return (ITexture)this["IconTexture"]; }
        }

        public IFontString Name
        {
            get { return (IFontString)this["Name"]; }
        }

        public IFontString Amount
        {
            get { return (IFontString)this["Amount"]; }
        }

        public IFontString Velocity
        {
            get { return (IFontString)this["Velocity"]; }
        }

        public IButton ResetButton
        {
            get { return (IButton)this["ResetButton"]; }
        }

        public IButton RemoveButton
        {
            get { return (IButton)this["RemoveButton"]; }
        }

        public IEntityId Id { get; set; }
    }
}