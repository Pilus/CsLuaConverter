namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;
    using System.Linq;

    public class FieldDeclarationVisitor : IVisitor<FieldDeclaration>
    {
        public void Visit(FieldDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members, '{0}', {{", element.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Field',");
            textWriter.WriteLine("scope = '{0}',", element.Scope);
            textWriter.WriteLine("static = {0},", (element.Static || element.Const).ToString().ToLower());
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void WriteDefaultValue(FieldDeclaration element, IndentedTextWriter textWriter, IProviders providers, bool @static)
        {
            if ((element.Static || element.Const) != @static)
            {
                return;
            }

            textWriter.Write("{0} = ", element.Name);

            if (element.Value != null)
            {
                VisitorList.Visit(element.Value);
                textWriter.WriteLine(",");
            }
            else
            { 
                textWriter.Write("_M.DV(");
                TypeOfExpressionVisitor.WriteTypeReference(element.Type, textWriter, providers);
                textWriter.WriteLine("),");
            }
        }

        public static void WriteInitializeValue(FieldDeclaration element, IndentedTextWriter textWriter, IProviders providers, bool @static)
        {
            if ((element.Static || element.Const) != @static)
            {
                return;
            }

            textWriter.WriteLine("if not(values.{0} == nil) then element[typeObject.Level].{0} = values.{0}; end", element.Name);
        }
    }
}