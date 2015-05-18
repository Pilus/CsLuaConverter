using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    internal interface IDefaultValueProvider
    {
        string GetDefaultValue(string typeName, bool isNullable);
        string GetDefaultValue(string typeName);
    }
}
