namespace GrindOMeter.IntegrationTests
{
    using System;
    using GrindOMeter;
    using GrindOMeter.View.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WoWSimulator;

    [TestClass]
    public class IntegrationTests
    {
        private static SessionBuilder PreprareSessionBuilder()
        {
            return new SessionBuilder()
                .WithAddOn(new GrindOMeterAddOn())
                .WithIgnoredXmlTemplate("UIDropDownMenuTemplate")
                .WithIgnoredXmlTemplate("UIPanelButtonTemplate")
                .WithXmlFile(@"View\Xml\GrindOMeterFrame.xml")
                .WithFrameWrapper("GrindOMeterFrame", GrindOMeterFrameWrapper.Init)
                .WithXmlFile(@"View\Xml\GrindOMeterTrackingRow.xml")
                .WithFrameWrapper("GrindOMeterTrackingRowTemplate", GrindOMeterTrackingRowWrapper.Init);
        }

        [TestMethod]
        public void IntegrationTestOverMultipleSessions()
        {
            var currencySystem = new CurrencySystem();
            currencySystem.Amounts[80] = 55;

            var session = PreprareSessionBuilder()
                .WithApiMock(currencySystem)
                .Build();

            session.RunStartup();

            session.RunUpdate();

            session.Actor.Click("Track");
            session.Actor.Click("Currencies");
            session.Actor.Click("CurrencyName80");

            session.RunUpdateForDuration(TimeSpan.FromSeconds(10));

            session.Actor.VerifyVisible("CurrencyName80", true);
            session.Actor.VerifyVisible("55", true);
            session.Actor.VerifyVisible("0.00 / hour", true);

            currencySystem.Amounts[80] = 56;
            session.RunUpdateForDuration(TimeSpan.FromSeconds(1));
            session.Actor.VerifyVisible("56", true);
            session.Actor.VerifyVisible("328.27 / hour", true);

            var row = session.GetGlobal<IGrindOMeterTrackingRow>("GrindOMeterTrackingRow1");
            row.ResetButton.Click();

            session.Actor.VerifyVisible("56", true);
            session.Actor.VerifyVisible("0.00 / hour", true);

            var savedSessionVariables = session.GetSavedVariables();
            session = null;

            var session2 = PreprareSessionBuilder()
                .WithApiMock(currencySystem)
                .WithSavedVariables(savedSessionVariables)
                .Build();

            session2.RunStartup();
            
            session2.RunUpdate();

            session2.Actor.VerifyVisible("CurrencyName80", true);
            session2.Actor.VerifyVisible("56", true);
            session2.Actor.VerifyVisible("0.00 / hour", true);
        }
    }
}
