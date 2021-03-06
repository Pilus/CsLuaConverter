﻿namespace GrindOMeter.UnitTests.View
{
    using System;
    using System.Collections.Generic;
    using BlizzardApi.Global;
    using BlizzardApi.WidgetEnums;
    using BlizzardApi.WidgetInterfaces;
    using CsLuaFramework.Wrapping;
    using GrindOMeter.Model.Entity;
    using GrindOMeter.Presenter;
    using GrindOMeter.View;
    using GrindOMeter.View.Xml;
    using Lua;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TestUtils;

    [TestClass]
    public class ViewTests
    {
        private static string TrackingRowTemplateXmlName = "GrindOMeterTrackingRowTemplate";
        private Mock<IApi> apiMock;
        private Mock<IGrindOMeterFrame> frameMock;
        private Mock<IEntitySelectionDropdownHandler> entitySelectionDropdownHandlerMock;
        private Mock<IWrapper> wrapperMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.apiMock = new Mock<IApi>();
            Global.Api = this.apiMock.Object;
            ApplyMockGlobalGetSet(this.apiMock);

            this.frameMock = new Mock<IGrindOMeterFrame>();
            var trackButton = MockButton(this.frameMock.Object);
            this.frameMock.SetupGet(f => f.TrackButton).Returns(trackButton.Object);

            Global.Api.SetGlobal("GrindOMeterFrame", this.frameMock.Object);

            this.entitySelectionDropdownHandlerMock = new Mock<IEntitySelectionDropdownHandler>();

            this.wrapperMock = new Mock<IWrapper>();
            this.wrapperMock.Setup(w => w.Wrap<IGrindOMeterFrame>("GrindOMeterFrame")).Returns(this.frameMock.Object);
        }

        [TestMethod]
        public void ViewShowsGrindOMeterFrameOnInit()
        {
            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            this.frameMock.Verify(f => f.Show(), Times.Once);
            this.frameMock.Verify(f => f.Hide(), Times.Never);
        }

        [TestMethod]
        public void ViewRegistersTrackingButtonClickWhenProvided()
        {
            var buttonMock = new Mock<IButton>();
            this.frameMock.SetupGet(frame => frame.TrackButton).Returns(buttonMock.Object);

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            var invoked = 0;
            Action clickAction = new Action(() => { invoked++; });

            Action<IButton> providedAction = null;
            buttonMock.Setup(f => f.SetScript(ButtonHandler.OnClick, It.IsAny<Action<IUIObject>>()))
                .Callback((ButtonHandler handler, Action<IButton> action) => providedAction = action);

            viewUnderTest.SetTrackButtonOnClick(clickAction);

            buttonMock.Verify(f => f.SetScript(ButtonHandler.OnClick, It.IsAny<Action<IUIObject>>()), Times.Once);
            Assert.IsTrue(providedAction != null, "SetScript action not received.");
            providedAction(buttonMock.Object);

            Assert.AreEqual(1, invoked);
        }

        [TestMethod]
        public void ViewAddsTrackingEntityRowsWhenAdded()
        {
            var containerMock = new Mock<IFrame>();
            this.frameMock.SetupGet(frame => frame.TrackingContainer).Returns(containerMock.Object);

            var frameProviderMock = new Mock<IFrameProvider>();
            Global.FrameProvider = frameProviderMock.Object;

            var trackingRowMocks = new List<Mock<IGrindOMeterTrackingRow>>();
            frameProviderMock.Setup(fp => fp.CreateFrame(FrameType.Frame, It.IsAny<string>(), containerMock.Object, TrackingRowTemplateXmlName))
                .Returns((FrameType frameType, string name, IRegion parent, string template) => {
                    var mock = CreateTrackingRowMock(parent);
                    trackingRowMocks.Add(mock);
                    return mock.Object;
            });

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityA", "IconA");
            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityB", "IconB");
            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityC", "IconC");

            Assert.AreEqual(3, trackingRowMocks.Count);
            Assert.AreEqual("EntityA", trackingRowMocks[0].Object.Name.GetText());
            Assert.AreEqual("IconA", trackingRowMocks[0].Object.IconTexture.GetTexture());
            Assert.AreEqual("EntityB", trackingRowMocks[1].Object.Name.GetText());
            Assert.AreEqual("IconB", trackingRowMocks[1].Object.IconTexture.GetTexture());
            Assert.AreEqual("EntityC", trackingRowMocks[2].Object.Name.GetText());
            Assert.AreEqual("IconC", trackingRowMocks[2].Object.IconTexture.GetTexture());

            ValidateAnchor(containerMock.Object, trackingRowMocks[0].Object);
            ValidateAnchor(trackingRowMocks[0].Object, trackingRowMocks[1].Object);
            ValidateAnchor(trackingRowMocks[1].Object, trackingRowMocks[2].Object);
        }

        [TestMethod]
        public void ViewRemovesTrackingEntityRowsWhenRemoved()
        {
            var containerMock = new Mock<IFrame>();
            this.frameMock.SetupGet(frame => frame.TrackingContainer).Returns(containerMock.Object);

            var frameProviderMock = new Mock<IFrameProvider>();
            Global.FrameProvider = frameProviderMock.Object;

            var trackingRowMocks = new List<Mock<IGrindOMeterTrackingRow>>();
            frameProviderMock.Setup(fp => fp.CreateFrame(FrameType.Frame, It.IsAny<string>(), containerMock.Object, TrackingRowTemplateXmlName))
                .Returns((FrameType frameType, string name, IRegion parent, string template) =>
                {
                    var mock = CreateTrackingRowMock(parent);
                    trackingRowMocks.Add(mock);
                    return mock.Object;
                });

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            var entityIdB = new Mock<IEntityId>().Object;
            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityA", "IconA");
            viewUnderTest.AddTrackingEntity(entityIdB, "EntityB", "IconB");
            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityC", "IconC");

            viewUnderTest.RemoveTrackingEntity(entityIdB);

            Assert.AreEqual(3, trackingRowMocks.Count);
            Assert.AreEqual("EntityA", trackingRowMocks[0].Object.Name.GetText());
            Assert.AreEqual("IconA", trackingRowMocks[0].Object.IconTexture.GetTexture());
            Assert.AreEqual("EntityC", trackingRowMocks[1].Object.Name.GetText());
            Assert.AreEqual("IconC", trackingRowMocks[1].Object.IconTexture.GetTexture());
            Assert.AreEqual(false, trackingRowMocks[2].Object.IsShown());

            viewUnderTest.AddTrackingEntity(new Mock<IEntityId>().Object, "EntityD", "IconD");

            Assert.AreEqual(3, trackingRowMocks.Count);
            Assert.AreEqual(true, trackingRowMocks[2].Object.IsShown());
            Assert.AreEqual("EntityD", trackingRowMocks[2].Object.Name.GetText());
            Assert.AreEqual("IconD", trackingRowMocks[2].Object.IconTexture.GetTexture());
        }

        [TestMethod]
        public void ViewShouldTriggerTheProvidedUpdateFunctionEvery100ms()
        {
            Action<IFrame> frameOnUpdate = null;
            this.frameMock.Setup(frame => frame.SetScript(FrameHandler.OnUpdate, It.IsAny<Action<IUIObject>>()))
                .Callback<FrameHandler, Action<IFrame>>((handler, action) => frameOnUpdate = action);

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            var startTime = 100000;
            Core.mockTime = startTime;

            var updateInvokes = 0;
            viewUnderTest.SetUpdateAction(() => updateInvokes++);

            Assert.IsTrue(frameOnUpdate != null, "Frame on update function is not registered");
            Assert.AreEqual(0, updateInvokes);

            frameOnUpdate(this.frameMock.Object);

            Assert.AreEqual(1, updateInvokes);

            frameOnUpdate(this.frameMock.Object);

            Assert.AreEqual(1, updateInvokes);

            Core.mockTime = startTime + 0.1;
            frameOnUpdate(this.frameMock.Object);

            Assert.AreEqual(2, updateInvokes);

            frameOnUpdate(this.frameMock.Object);

            Assert.AreEqual(2, updateInvokes);
        }

        [TestMethod]
        public void ViewTriggersHandlersForButtons()
        {
            var containerMock = new Mock<IFrame>();
            this.frameMock.SetupGet(frame => frame.TrackingContainer).Returns(containerMock.Object);

            var frameProviderMock = new Mock<IFrameProvider>();
            Global.FrameProvider = frameProviderMock.Object;

            var trackingRowMocks = new List<Mock<IGrindOMeterTrackingRow>>();
            frameProviderMock.Setup(fp => fp.CreateFrame(FrameType.Frame, It.IsAny<string>(), containerMock.Object, TrackingRowTemplateXmlName))
                .Returns((FrameType frameType, string name, IRegion parent, string template) => {
                    var mock = CreateTrackingRowMock(parent);
                    trackingRowMocks.Add(mock);
                    return mock.Object;
                });

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            IEntityId resetId = null;
            IEntityId removeId = null;

            viewUnderTest.SetTrackingEntityHandlers(id => resetId = id, id => removeId = id);

            var idA = new Mock<IEntityId>().Object;
            var idB = new Mock<IEntityId>().Object;
            var idC = new Mock<IEntityId>().Object;
            viewUnderTest.AddTrackingEntity(idA, "EntityA", "IconA");
            viewUnderTest.AddTrackingEntity(idB, "EntityB", "IconB");
            viewUnderTest.AddTrackingEntity(idC, "EntityC", "IconC");

            trackingRowMocks[1].Object.ResetButton.Click();
            Assert.AreEqual(idB, resetId);

            trackingRowMocks[2].Object.RemoveButton.Click();
            Assert.AreEqual(idC, removeId);
        }

        [TestMethod]
        public void ViewUpdatesTrackingEntityVelocity()
        {
            var containerMock = new Mock<IFrame>();
            this.frameMock.SetupGet(frame => frame.TrackingContainer).Returns(containerMock.Object);

            var frameProviderMock = new Mock<IFrameProvider>();
            Global.FrameProvider = frameProviderMock.Object;

            var trackingRowMocks = new List<Mock<IGrindOMeterTrackingRow>>();
            frameProviderMock.Setup(fp => fp.CreateFrame(FrameType.Frame, It.IsAny<string>(), containerMock.Object, TrackingRowTemplateXmlName))
                .Returns((FrameType frameType, string name, IRegion parent, string template) => {
                    var mock = CreateTrackingRowMock(parent);
                    trackingRowMocks.Add(mock);
                    return mock.Object;
                });

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            viewUnderTest.AddTrackingEntity(new EntityId(EntityType.Item, 1), "EntityA", "IconA");
            viewUnderTest.AddTrackingEntity(new EntityId(EntityType.Item, 2), "EntityB", "IconB");
            viewUnderTest.AddTrackingEntity(new EntityId(EntityType.Item, 3), "EntityC", "IconC");

            viewUnderTest.UpdateTrackingEntityVelocity(new EntityId(EntityType.Item, 2), 43, 3.1415);

            Assert.AreEqual("43", trackingRowMocks[1].Object.Amount.GetText());
            Assert.AreEqual("3.14 / hour", trackingRowMocks[1].Object.Velocity.GetText());
        }

        [TestMethod]
        public void ViewShowsEntitySelection()
        {
            IFrame shownAnchor = null;
            IEntitySelection shownSelection = null;

            this.entitySelectionDropdownHandlerMock.Setup(es =>
                es.Show(It.IsAny<IFrame>(), It.IsAny<IEntitySelection>()))
                .Callback((IFrame anchor, IEntitySelection selection) => {
                    shownAnchor = anchor;
                    shownSelection = selection;
                });

            var viewUnderTest = new View(this.entitySelectionDropdownHandlerMock.Object, this.wrapperMock.Object);

            var entitySelectionMock = new Mock<IEntitySelection>();

            viewUnderTest.ShowEntitySelection(entitySelectionMock.Object);

            this.entitySelectionDropdownHandlerMock.Verify(es => 
                es.Show(It.IsAny<IFrame>(), It.IsAny<IEntitySelection>()), Times.Once());

            Assert.AreEqual(this.frameMock.Object.TrackButton, shownAnchor);
            Assert.AreEqual(entitySelectionMock.Object, shownSelection);

        }

        private static void ValidateAnchor(IGrindOMeterTrackingRow expectedAnchor, IGrindOMeterTrackingRow row)
        {
            Assert.AreEqual(2, row.GetNumPoints());

            var point1 = row.GetPoint(1);
            Assert.AreEqual(FramePoint.TOPLEFT, point1.Value1);
            Assert.AreEqual(expectedAnchor, point1.Value2);
            Assert.AreEqual(FramePoint.BOTTOMLEFT, point1.Value3);
            Assert.AreEqual(0, point1.Value4);
            Assert.AreEqual(0, point1.Value5);

            var point2 = row.GetPoint(2);
            Assert.AreEqual(FramePoint.TOPRIGHT, point2.Value1);
            Assert.AreEqual(expectedAnchor, point1.Value2);
            Assert.AreEqual(FramePoint.BOTTOMRIGHT, point2.Value3);
            Assert.AreEqual(0, point2.Value4);
            Assert.AreEqual(0, point2.Value5);
        }

        private static void ValidateAnchor(IFrame expectedAnchorContainer, IGrindOMeterTrackingRow row)
        {
            Assert.AreEqual(2, row.GetNumPoints());

            var point1 = row.GetPoint(1);
            Assert.AreEqual(FramePoint.TOPLEFT, point1.Value1);
            Assert.AreEqual(expectedAnchorContainer, point1.Value2);
            Assert.AreEqual(FramePoint.TOPLEFT, point1.Value3);
            Assert.AreEqual(0, point1.Value4);
            Assert.AreEqual(0, point1.Value5);

            var point2 = row.GetPoint(2);
            Assert.AreEqual(FramePoint.TOPRIGHT, point2.Value1);
            Assert.AreEqual(expectedAnchorContainer, point1.Value2);
            Assert.AreEqual(FramePoint.TOPRIGHT, point2.Value3);
            Assert.AreEqual(0, point2.Value4);
            Assert.AreEqual(0, point2.Value5);

        }

        private static Mock<IGrindOMeterTrackingRow> CreateTrackingRowMock(IRegion parent)
        {
            var mock = new Mock<IGrindOMeterTrackingRow>();

            var nameMock = MockFontString();
            mock.SetupGet(row => row.Name).Returns(nameMock.Object);
            var amountMock = MockFontString();
            mock.SetupGet(row => row.Amount).Returns(amountMock.Object);
            var velocityMock = MockFontString();
            mock.SetupGet(row => row.Velocity).Returns(velocityMock.Object);
            var iconTextureMock = MockTexture();
            mock.SetupGet(row => row.IconTexture).Returns(iconTextureMock.Object);
            var resetButtonMock = MockButton(mock.Object);
            mock.SetupGet(row => row.ResetButton).Returns(resetButtonMock.Object);
            var removeButtonMock = MockButton(mock.Object);
            mock.SetupGet(row => row.RemoveButton).Returns(removeButtonMock.Object);

            IEntityId id = null;
            mock.SetupSet(p => p.Id = It.IsAny<IEntityId>())
                .Callback<IEntityId>((value) => {
                    id = value;
                });
            mock.SetupGet(p => p.Id).Returns(() => {
                return id;
                });

            var points = new List<Dictionary<string, object>>();
            mock.Setup(row => row.SetPoint(It.IsAny<FramePoint>(), It.IsAny<IRegion>(), It.IsAny<FramePoint>()))
                .Callback((FramePoint point, IRegion p, FramePoint parentPoint) => points.Add(new Dictionary<string, object>()
                {
                    { "point", point }, { "parent", p }, {"parentPoint", parentPoint }, { "x", 0.0 }, { "y", 0.0 }
                }));
            mock.Setup(row => row.GetNumPoints()).Returns(() => points.Count);
            mock.Setup(row => row.GetPoint(It.IsAny<int>()))
                .Returns((int index) =>
                {
                    if (index < 1 || index > points.Count) return null;
                    var point = points[index - 1];
                    return TestUtil.StructureMultipleValues<FramePoint, IRegion, FramePoint?, double?, double?>
                        ((FramePoint)point["point"], (IRegion)point["parent"], (FramePoint)point["parentPoint"], (double)point["x"], (double)point["y"]);
                });

            var shown = true;
            mock.Setup(row => row.Show()).Callback(() => { shown = true; });
            mock.Setup(row => row.Hide()).Callback(() => { shown = false; });
            mock.Setup(row => row.IsShown()).Returns(() => shown);

            return mock;
        }

        private static Mock<IFontString> MockFontString()
        {
            string text = string.Empty;
            var mock = new Mock<IFontString>();
            mock.Setup(fs => fs.SetText(It.IsAny<string>()))
                .Callback((string s) => text = s);
            mock.Setup(fs => fs.GetText())
                .Returns(() => text);
            return mock;
        }

        private static Mock<ITexture> MockTexture()
        {
            string texture = string.Empty;
            var mock = new Mock<ITexture>();
            mock.Setup(fs => fs.SetTexture(It.IsAny<string>()))
                .Callback((string s) => texture = s);
            mock.Setup(fs => fs.GetTexture())
                .Returns(() => texture);
            return mock;
        }

        private static Mock<IButton> MockButton(IFrame parent)
        {
            string text = string.Empty;
            var mock = new Mock<IButton>();
            mock.Setup(b => b.SetText(It.IsAny<string>()))
                .Callback((string s) => text = s);
            mock.Setup(b => b.GetText())
                .Returns(() => text);

            Action<IButton> clickAction = null;
            mock.Setup(b => b.SetScript(ButtonHandler.OnClick, It.IsAny<Action<IUIObject>>()))
                .Callback((ButtonHandler handler, Action<IUIObject> action) => clickAction = action);
            mock.Setup(b => b.Click()).Callback(() => clickAction(mock.Object));

            mock.Setup(b => b.GetParent()).Returns(parent);

            return mock;
        }

        private static void ApplyMockGlobalGetSet(Mock<IApi> apiMock)
        {
            var globalObjects = new Dictionary<string, object>();

            apiMock.Setup(api => api.SetGlobal(It.IsAny<string>(), It.IsAny<object>()))
                .Callback((string key, object obj) => { globalObjects[key] = obj; });
            apiMock.Setup(api => api.GetGlobal(It.IsAny<string>()))
                .Returns((string key) => globalObjects.ContainsKey(key) ? globalObjects[key] : null);
        }
    }
}
