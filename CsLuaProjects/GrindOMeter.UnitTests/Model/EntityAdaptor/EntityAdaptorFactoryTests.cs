namespace GrindOMeter.UnitTests.Model.EntityAdaptor
{
    using GrindOMeter.Model.Entity;
    using GrindOMeter.Model.EntityAdaptor;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class EntityAdaptorFactoryTests
    {
        [TestMethod()]
        public void CreateAdoptorCreatesCorrectAdaptors()
        {
            var factoryUnderTest = new EntityAdaptorFactory();

            Assert.IsTrue(factoryUnderTest.CreateAdoptor(EntityType.Currency) is CurrencyAdaptor);
            Assert.IsTrue(factoryUnderTest.CreateAdoptor(EntityType.Item) is ItemAdaptor);
        }
    }
}