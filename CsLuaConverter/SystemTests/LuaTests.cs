namespace SystemTests
{
    using System;

    using Lua;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LuaTests
    {
        [TestMethod]
        public void TestStrSub()
        {
            var str = "abcd";

            Assert.AreEqual("",    Strings.strsub(str, 0, 0));
            Assert.AreEqual("a",   Strings.strsub(str, 0, 1));
            Assert.AreEqual("ab",  Strings.strsub(str, 0, 2));
            Assert.AreEqual("abc", Strings.strsub(str, 0, 3));

            Assert.AreEqual("",    Strings.strsub(str, 1, 0));
            Assert.AreEqual("a",   Strings.strsub(str, 1, 1));
            Assert.AreEqual("ab",  Strings.strsub(str, 1, 2));
            Assert.AreEqual("abc", Strings.strsub(str, 1, 3));

            Assert.AreEqual("",    Strings.strsub(str, 2, 0));
            Assert.AreEqual("",    Strings.strsub(str, 2, 1));
            Assert.AreEqual("b",   Strings.strsub(str, 2, 2));
            Assert.AreEqual("bc",  Strings.strsub(str, 2, 3));

            Assert.AreEqual("",    Strings.strsub(str, 3, 0));
            Assert.AreEqual("",    Strings.strsub(str, 3, 1));
            Assert.AreEqual("",    Strings.strsub(str, 3, 2));
            Assert.AreEqual("c",   Strings.strsub(str, 3, 3));
        }
    }
}
