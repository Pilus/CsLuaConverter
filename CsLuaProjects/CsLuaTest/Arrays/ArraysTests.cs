﻿namespace CsLuaTest.Arrays
{
    using System;

    public class ArraysTests : BaseTest
    {
        public ArraysTests()
        {
            this.Name = "Arrays";
            this.Tests["ArrayImplementsInterfaces"] = ArrayImplementsInterfaces;
            this.Tests["ArrayInitializationAndAmbigurity"] = ArrayInitializationAndAmbigurity;
            this.Tests["ArraysAsMethodArgument"] = ArraysAsMethodArgument;
            this.Tests["ArrayImplementedWithSpecificLength"] = ArrayImplementedWithSpecificLength;
            this.Tests["ArrayImplementedWithSpecificLengthInClass"] = ArrayImplementedWithSpecificLengthInClass;
        }

        private static void ArrayImplementsInterfaces()
        {
            var a = new string[] {"a"};
            Assert(true, a is string[]);
            Assert(true, a is System.Array);
            Assert(true, a is System.Collections.IList);
            Assert(true, a is System.Collections.Generic.IList<string>);
            Assert(true, a is System.Collections.ICollection);
            Assert(true, a is System.Collections.Generic.ICollection<string>);
            Assert(true, a is System.Collections.IEnumerable);
            Assert(true, a is System.Collections.Generic.IEnumerable<string>);
        }

        private static void ArrayInitializationAndAmbigurity()
        {
            var arrayClass = new ClassWithArrays();

            var a1 = new string[] {};
            Assert("string", arrayClass.TypeDependent(a1));
            Assert(0, a1.Length);

            var a2 = new[] { "abc", "def" };
            Assert("string", arrayClass.TypeDependent(a2));

            var a3 = new[] { 1, 3 };
            Assert("int", arrayClass.TypeDependent(a3));

            var a3b = new[] { 1.1, 3.2 };
            Assert("double", arrayClass.TypeDependent(a3b));

            var a4 = new object[] { true, 1, "ok" };
            Assert("object", arrayClass.TypeDependent(a4));

            var a5 = new[] {new AClass<int>() {Value = 4}, new AClass<int>() { Value = 6 } };
            Assert("Aint", arrayClass.TypeDependent(a5));

            var a6 = new[] { new AClass<string>() };
            Assert("Astring", arrayClass.TypeDependent(a6));

            var a7 = new AClass<string>[] {};
            Assert("Astring", arrayClass.TypeDependent(a7));
        }

        private static void ArraysAsMethodArgument()
        {
            var arrayClass = new ClassWithArrays();
            var array = new [] { "abc", "def"};

            Assert(2, arrayClass.GetLengthOfStringArray(array));
        }

        public static void ArrayImplementedWithSpecificLength()
        {
            var array = new string[4];
            Assert(4, array.Length);
            array[2] = "ok";
            Assert("ok", array[2]);
        }

        public static void ArrayImplementedWithSpecificLengthInClass()
        {
            var c = new ClassWithPredefinedArray();
            Assert(3, c.Array1.Length);
            Assert(5, c.Array2.Length);
        }
    }
}