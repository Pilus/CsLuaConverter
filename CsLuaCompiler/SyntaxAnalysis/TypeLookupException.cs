﻿namespace CsToLua.SyntaxAnalysis
{
    using System;

    public class TypeLookupException : Exception
    {
        public TypeLookupException(string msg) : base(msg)
        {
            
        }
    }
}