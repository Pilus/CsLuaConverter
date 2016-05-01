namespace CsLuaTest
{
    using System;
    public class TestIgnoredException : Exception
    {
        public TestIgnoredException() : base("Test ignored.")
        {
            
        }
    }
}