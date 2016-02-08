namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public class PropertyDeclarationVisitor : IVisitor<PropertyDeclaration>
    {
        public void Visit(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members, '{0}',{{", element.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = '{0}',", IsAutoProperty(element) ? "AutoProperty" : "Property");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = {0},", element.Static.ToString().ToLower());
            WriteGetSet(element, textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void WriteInitializeValue(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Static)
            {
                return;
            }

            if (!element.Accessors.HasAutoSetter)
            {
                return;
            }

            textWriter.WriteLine("if not(values.{0} == nil) then element[typeObject.Level].{0} = values.{0}; end", element.Name);
        }

        public static void WriteDefaultValue(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers, bool @static)
        {
            if ((element.Static) != @static || !IsAutoProperty(element))
            {
                return;
            }

            textWriter.Write("{0} = _M.DV(", element.Name);
            TypeOfExpressionVisitor.WriteTypeReference(element.Type, textWriter, providers);

            textWriter.WriteLine("),");
        }

        private static bool IsAutoProperty(PropertyDeclaration element)
        {
            return element.Accessors.HasAutoGetter && element.Accessors.HasAutoSetter;
        }

        private static void WriteGetSet(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (IsAutoProperty(element))
            {
                return;
            }

            if (element.Accessors.GetBlock != null)
            {
                textWriter.WriteLine("get = function(element)");
                VisitorList.Visit(element.Accessors.GetBlock);
                textWriter.WriteLine("end,");
            }

            if (element.Accessors.SetBlock != null)
            {
                var scope = providers.NameProvider.CloneScope();
                providers.NameProvider.AddToScope(new ScopeElement("value"));
                textWriter.WriteLine("set = function(element, value)");
                VisitorList.Visit(element.Accessors.SetBlock);
                textWriter.WriteLine("end,");
                providers.NameProvider.SetScope(scope);
            }
        }
    }
}