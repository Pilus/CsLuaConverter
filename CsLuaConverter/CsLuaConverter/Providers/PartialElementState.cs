namespace CsLuaConverter.Providers
{
    public class PartialElementState
    {
        public int? CurrentState { get; set; }
        public int? NextState { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public int NumberOfGenerics { get; set; }
        public bool DefinedConstructorWritten { get; set; }
    }
}