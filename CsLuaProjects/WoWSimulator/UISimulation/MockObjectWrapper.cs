namespace WoWSimulator.UISimulation
{
    using System;
    using BlizzardApi.Global;
    using CsLuaFramework.Wrapping;
    using Lua;

    public class MockObjectWrapper : IWrapper
    {
        private IApi api;

        public MockObjectWrapper(IApi api)
        {
            this.api = api;
        }

        public NativeLuaTable Unwrap<T>(string globalVarName) where T : class
        {
            throw new NotImplementedException();
        }

        public T Wrap<T>(string globalVarName) where T : class
        {
            var obj = this.api.GetGlobal(globalVarName);
            throw new NotImplementedException();
        }

        public T Wrap<T>(NativeLuaTable luaTable) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
