namespace CsToLua.SyntaxAnalysis.ClassElements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;

    internal class Serializability : ILuaElement
    {
        private readonly bool isSerializable;
        private readonly bool isStatic;
        private readonly List<Property> properties;
        private readonly List<ClassVariable> variables;
        private readonly string className;

        public Serializability(bool isStatic, bool isSerializable, List<Property> properties,
            List<ClassVariable> variables, string className)
        {
            this.isStatic = isStatic;
            this.isSerializable = isSerializable;
            this.properties = properties;
            this.variables = variables;
            this.className = className;
        }

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            if (!this.isSerializable || this.isStatic)
            {
                return;
            }

            IEnumerable<Property> serializeableProperties = this.properties
                .Where(p => p.IsDefault()); // TODO: And not nonserializable

            var serializeableElements = new Dictionary<string, string>();
            serializeableProperties.ToList()
                .ForEach(prop => serializeableElements[prop.Name] = prop.Type.GetTypeString());
            this.variables.ForEach(variable => serializeableElements[variable.Name] = variable.Type.GetTypeString());

            LuaFormatter.WriteClassElement(textWriter, ElementType.Serialization, "serialize", false, false, () =>
            {
                textWriter.WriteLine("function(info)");
                textWriter.Indent++;
                foreach (var element in serializeableElements)
                {
                    textWriter.WriteLine("info.AddValue(class, '{0}', class.{0}, '{1}');", element.Key, element.Value);
                }
                textWriter.Indent--;
                textWriter.Write("end");
            }, this.className);
            LuaFormatter.WriteClassElement(textWriter, ElementType.Serialization, "deserialize", false, false, () =>
            {
                textWriter.WriteLine("function(info)");
                textWriter.Indent++;
                foreach (var element in serializeableElements)
                {
                    textWriter.WriteLine("class.{0} = info.GetValue(class, '{0}', '{1}');", element.Key, element.Value);
                }
                textWriter.Indent--;
                textWriter.Write("end");
            }, this.className);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }
    }
}