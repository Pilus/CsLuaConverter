﻿namespace CsLuaTest.Wrap
{
    public interface ISimpleInterface
    {
        string Method(string input);

        int Value { get; set; }

        void MethodVoid(bool value);
    }
}