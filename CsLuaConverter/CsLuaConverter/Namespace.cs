namespace CsLuaConverter
{
    using System;

    public class Namespace
    {
        public string Name;

        public Action<IIndentedTextWriterWrapper> WritingAction;
    }
}