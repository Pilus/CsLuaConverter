﻿namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class PropertyDeclarationVisitor : IVisitor<PropertyDeclaration>
    {
        public void Visit(PropertyDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members, '{0}',{{", element.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Property',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = '{0}',", element.Static.ToString().ToLower());
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
    }
}