namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System;
    using System.Linq;

    public class TypeResult
    {
        public string AdditionalString;
        public Type Type;

        private static string StripGenericsFromType(string name)
        {
            return name.Split('`').First();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.AdditionalString))
            {
                return StripGenericsFromType(this.Type.FullName);
            }

            if (this.Type.IsEnum)
            {
                return this.Type.FullName + "." + this.AdditionalString;
            }

            return StripGenericsFromType(this.Type.FullName) + "." + this.AdditionalString;
        }
    }
}