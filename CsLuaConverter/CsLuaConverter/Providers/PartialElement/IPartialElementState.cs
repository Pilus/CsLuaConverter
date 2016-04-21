namespace CsLuaConverter.Providers.PartialElement
{
    public interface IPartialElementState
    {
        int? CurrentState { get; set; }
        bool IsFirst { get; set; }
        bool IsLast { get; set; }
        int? NextState { get; set; }
        int NumberOfGenerics { get; set; }
    }
}