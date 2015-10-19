
namespace CsLuaConverter.SyntaxAnalysis
{
    public interface IPartialLuaElement : ILuaElement
    {
        string Name { get; }
        bool IsPartial { get; }
    }
}
