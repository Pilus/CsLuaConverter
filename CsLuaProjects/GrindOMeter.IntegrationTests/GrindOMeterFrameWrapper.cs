namespace GrindOMeter.IntegrationTests
{
    using BlizzardApi.WidgetInterfaces;
    using GrindOMeter.View.Xml;
    using WoWSimulator.UISimulation;
    using WoWSimulator.UISimulation.UiObjects;
    using WoWSimulator.UISimulation.XMLHandler;

    public class GrindOMeterFrameWrapper : Frame, IGrindOMeterFrame
    {
        public static IUIObject Init(UiInitUtil util, LayoutFrameType layout, IRegion parent)
        {
            return new GrindOMeterFrameWrapper(util, layout, parent);
        }

        public GrindOMeterFrameWrapper(UiInitUtil util, LayoutFrameType layout, IRegion parent)
            : base(util, "frame", layout as FrameType, parent)
        { }

        public IFontString Label
        {
            get { return (IFontString) this["Label"]; }
        }

        public IButton TrackButton
        {
            get { return (IButton)this["TrackButton"]; }
        }

        public IFrame TrackingContainer
        {
            get { return (IFrame)this["TrackingContainer"]; }
        }
    }
}