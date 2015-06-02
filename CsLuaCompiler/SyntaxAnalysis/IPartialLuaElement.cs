
namespace CsLuaCompiler.SyntaxAnalysis
{
    interface IPartialLuaElement : ILuaElement
    {
        string Name { get; }
        bool IsPartial { get; }
    }
}
