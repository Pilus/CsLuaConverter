namespace CsLuaConverter.Providers.PartialElement
{
    public class PartialElementState : IPartialElementState
    {
        public int? CurrentState { get; set; }
        public int? NextState { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public int NumberOfGenerics { get; set; }
    }
}