namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class BaseList : ILuaElement
    {
        public VariableName name;

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            if (!this.IsInterface(nameProvider))
            {
                this.name.WriteLua(textWriter, nameProvider);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(BaseListSyntax), token.Parent);
            token = token.GetNextToken();
            this.name = new VariableName(true, false, false);
            return this.name.Analyze(token);
        }

        public bool IsInterface(FullNameProvider nameProvider)
        {
            return this.name.LookupType(nameProvider).IsInterface;
        }

        public void AddInheritiedMembers(FullNameProvider nameProvider)
        {
            nameProvider.AddAllInheritedMembersToScope(this.name.LookupType(nameProvider));
        }

        public string GetNameString(FullNameProvider nameProvider)
        {
            return "'" + this.name.LookupType(nameProvider).Name.Split('`').First() + "'";
        }

        public string GetFullNameString(FullNameProvider nameProvider)
        {
            return "'" + this.name.LookupType(nameProvider).FullName + "'";
        }

        public string GetName(FullNameProvider nameProvider)
        {
            return this.name.LookupType(nameProvider).Name.Split('`').First();
        }
    }
}