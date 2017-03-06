
namespace SystemTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ArgumentNullExceptionTests
    {
        [TestMethod]
        public void TestARgumentNullExceptionWithEmptyCstor()
        {
            var referenceException = new ArgumentNullException();
            var exceptionUnderTest = new SystemZZZ.ArgumentNullException();

            Assert.AreEqual(referenceException.Message, exceptionUnderTest.Message);
            Assert.AreEqual(referenceException.ToString(), exceptionUnderTest.ToString());
        }
    }
}
