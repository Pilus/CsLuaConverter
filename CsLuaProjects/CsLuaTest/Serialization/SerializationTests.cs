namespace CsLuaTest.Serialization
{
    using System.Collections.Generic;
    using CsLuaFramework;
    using Lua;

    public class SerializationTests : BaseTest
    {
        public SerializationTests()
        {
            Name = "Serialization";
            this.Tests["TestBasicSerializableClass"] = TestBasicSerializableClass;
            this.Tests["TestClassWithSubObject"] = TestClassWithSubObject;
            this.Tests["TestClassInList"] = TestClassInList;
            this.Tests["TestSerializeDictionary"] = TestSerializeDictionary;
        }
        
        private static void TestBasicSerializableClass()
        {
            var serializer = new Serializer();

            var theClass = new ClassWithNativeObjects();

            var res = serializer.Serialize(theClass);

            Assert(theClass.AString, res["2_AString"]);
            Assert(theClass.ANumber, res["2_ANumber"]);
            Assert("CsLuaTest.Serialization.ClassWithNativeObjects", res["type"]);

            var processedClass = serializer.Deserialize<ClassWithNativeObjects>(res);

            Assert(theClass.AString, processedClass.AString);
            Assert(theClass.ANumber, processedClass.ANumber);
        }
        
        private static void TestClassWithSubObject()
        {
            var serializer = new Serializer();

            var theClass = new ClassWithSubObject();

            var res = serializer.Serialize(theClass);

            Assert("CsLuaTest.Serialization.ClassWithSubObject", res["type"]);

            var arrayRes = res["2_AnArray"] as NativeLuaTable;
            Assert("System.String[]", arrayRes["type"]);
            Assert(theClass.AnArray[0], arrayRes["2#_0"]);
            Assert(theClass.AnArray[1], arrayRes["2#_1"]);

            var subRes = res["2_AClass"] as NativeLuaTable;
            Assert("CsLuaTest.Serialization.ClassWithNativeObjects", subRes["type"]);
            Assert(theClass.AClass.AString, subRes["2_AString"]);
            Assert(theClass.AClass.ANumber, subRes["2_ANumber"]);

            var processedClass = serializer.Deserialize<ClassWithSubObject>(res);

            Assert(theClass.AnArray[0], processedClass.AnArray[0]);
            Assert(theClass.AnArray[1], processedClass.AnArray[1]);
            Assert(theClass.AClass.AString, processedClass.AClass.AString);
            Assert(theClass.AClass.ANumber, processedClass.AClass.ANumber);
        }

        private static void TestClassInList()
        {
            var theClass = new ClassWithNativeObjects();
            var list = new List<ClassWithNativeObjects>()
            {
                theClass,
            };

            var serializer = new Serializer();

            var res = serializer.Serialize(list);

            Assert("System.Collections.Generic.List`1[CsLuaTest.Serialization.ClassWithNativeObjects]", res["type"]);

            var subRes = res["2#_0"] as NativeLuaTable;
            Assert(theClass.AString, subRes["2_AString"]);
            Assert(theClass.ANumber, subRes["2_ANumber"]);

            var processedClass = serializer.Deserialize<List<ClassWithNativeObjects>>(res);

            Assert(1, processedClass.Count);
            var res1 = processedClass[0];
            Assert(theClass.AString, res1.AString);

            var res2 = processedClass[0].AString;
            Assert(theClass.AString, res2);
            Assert(theClass.ANumber, processedClass[0].ANumber);
        }

        private static void TestSerializeDictionary()
        {
            var serializer = new Serializer();

            var dict = new Dictionary<object, object>()
            {
                { 43, "something" },
                { "an index", "Someting else" }
            };

            var res = serializer.Serialize(dict);

            Assert(dict[43], res["2#_43"]);
            Assert(dict["an index"], res["2_an index"]);

            var processedDict = serializer.Deserialize<Dictionary<object, object>>(res);

            Assert(dict[43], processedDict[43]);
            Assert(dict["an index"], processedDict["an index"]);
        }
    }
}