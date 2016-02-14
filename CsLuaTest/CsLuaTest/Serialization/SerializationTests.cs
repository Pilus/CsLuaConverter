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
            this.Tests["TestClassInCsLuaList"] = TestClassInCsLuaList;
            this.Tests["TestSerializeDictionary"] = TestSerializeDictionary;
        }
        
        private static void TestBasicSerializableClass()
        {
            var theClass = new ClassWithNativeObjects();

            var res = Serializer.Serialize(theClass);

            Assert(theClass.AString, (res[2] as NativeLuaTable)["AString"]);
            Assert(theClass.ANumber, (res[2] as NativeLuaTable)["ANumber"]);
            Assert(223500, res["type"]);

            var processedClass = Serializer.Deserialize<ClassWithNativeObjects>(res);

            Assert(theClass.AString, processedClass.AString);
            Assert(theClass.ANumber, processedClass.ANumber);
        }
        
        private static void TestClassWithSubObject()
        {
            var theClass = new ClassWithSubObject();

            var res = Serializer.Serialize(theClass);

            Assert("CsLuaTest.Serialization.ClassWithSubObject", res["__type"]);
            var arrayRes = res["AnArray"] as NativeLuaTable;
            //Assert("System.String[]", arrayRes["__type"]);
            Assert(theClass.AnArray[0], arrayRes[0]);
            Assert(theClass.AnArray[1], arrayRes[1]);

            var subRes = res["AClass"] as NativeLuaTable;
            Assert("CsLuaTest.Serialization.ClassWithNativeObjects", subRes["__type"]);
            Assert(theClass.AClass.AString, subRes["AString"]);
            Assert(theClass.AClass.ANumber, subRes["ANumber"]);

            var processedClass = Serializer.Deserialize<ClassWithSubObject>(res);

            Assert(theClass.AnArray[0], processedClass.AnArray[0]);
            Assert(theClass.AnArray[1], processedClass.AnArray[1]);
            Assert(theClass.AClass.AString, processedClass.AClass.AString);
            Assert(theClass.AClass.ANumber, processedClass.AClass.ANumber);
        }

        private static void TestClassInCsLuaList()
        {
            var theClass = new ClassWithNativeObjects();
            var list = new List<ClassWithNativeObjects>()
            {
                theClass,
            };

            var res = Serializer.Serialize(list);

            Assert(1, res["__size"]);
            var subRes = res[0] as NativeLuaTable;
            Assert(theClass.AString, subRes["AString"]);
            Assert(theClass.ANumber, subRes["ANumber"]);

            var processedClass = Serializer.Deserialize<List<ClassWithNativeObjects>>(res);

            Assert(1, processedClass.Count);
            var res1 = processedClass[0];
            Assert(theClass.AString, res1.AString);

            var res2 = processedClass[0].AString;
            Assert(theClass.AString, res2);
            Assert(theClass.ANumber, processedClass[0].ANumber);
        }

        private static void TestSerializeDictionary()
        {
            var dict = new Dictionary<object, object>()
            {
                { 43, "something" },
                { "an index", "Someting else" }
            };

            var res = Serializer.Serialize(dict);

            Assert(dict[43], res[43]);
            Assert(dict["an index"], res["an index"]);

            var processedDict = Serializer.Deserialize<Dictionary<object, object>>(res);

            Assert(dict[43], processedDict[43]);
            Assert(dict["an index"], processedDict["an index"]);
        }
    }
}