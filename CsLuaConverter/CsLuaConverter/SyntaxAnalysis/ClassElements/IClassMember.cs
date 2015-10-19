namespace CsLuaConverter.SyntaxAnalysis.ClassElements
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Providers;

    public interface IClassMember
    {
         string Name { get; }
         string MemberType { get; }
         Scope Scope { get; }
         bool Static { get; }
         void AddValues(Dictionary<string, object> values, IndentedTextWriter textWriter, IProviders providers);
    }
}