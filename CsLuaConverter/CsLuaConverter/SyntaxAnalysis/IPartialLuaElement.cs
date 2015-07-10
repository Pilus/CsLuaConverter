
namespace CsLuaConverter.SyntaxAnalysis
{
    interface IPartialLuaElement : ILuaElement
    {
        string Name { get; }
        bool IsPartial { get; }
    }
}
