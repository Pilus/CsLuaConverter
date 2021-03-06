﻿namespace CsLuaTest
{
    using System;
    using System.Collections.Generic;
    using Lua;

    public abstract class BaseTest : ITestSuite
    {
        public string Name = "Unnamed";
        public static string Output = "";
        public const bool ContinueOnError = false;
        public static int TestCount;
        public static int FailCount;
        public static int IgnoreCount;

        public Dictionary<string, Action> Tests
        {
            get; protected set;
        }

        public BaseTest()
        {
            this.Tests = new Dictionary<string, Action>();
        }

        public void PerformTests(IndentedLineWriter lineWriter)
        {
            lineWriter.WriteLine(Name);
            lineWriter.indent++;
            foreach (var testName in this.Tests.Keys)
            {
                var test = this.Tests[testName];

                if (ContinueOnError)
                {
                    try
                    {
                        TestCount++;
                        ResetOutput();
                        test();
                        lineWriter.WriteLine(testName + " Success");
                    }
                    catch (TestIgnoredException ex)
                    {
                        IgnoreCount++;
                        lineWriter.WriteLine(testName + " Ignored");
                    }
                    catch (Exception ex)
                    {
                        FailCount++;
                        lineWriter.WriteLine(testName + " Failed");
                        lineWriter.indent++;
                        foreach (var errorLine in ex.Message.Split('\n'))
                        {
                            lineWriter.WriteLine(errorLine);
                        }
                        
                        lineWriter.indent--;
                    }
                }
                else
                {
                    TestCount++;
                    lineWriter.WriteLine(testName);
                    ResetOutput();
                    test();
                }
            }
            lineWriter.indent--;
        }

        protected static void ResetOutput()
        {
            Output = "";
        }

        protected static void Assert(object expectedValueObj, object actualValueObj, string additionalString = null)
        {
            var expectedValue = Strings.tostring(expectedValueObj);
            var actualValue = Strings.tostring(actualValueObj);

            if (expectedValue != actualValue)
            {
                throw new Exception(Strings.format("Incorrect value. Expected: '{0}' got: '{1}'. {2}", expectedValue, actualValue, additionalString ?? string.Empty));
            }
        }
    }
}