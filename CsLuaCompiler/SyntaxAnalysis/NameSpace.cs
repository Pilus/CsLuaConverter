namespace CsLuaCompiler.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using Lua;
    using Microsoft.CodeAnalysis;

    internal class NameSpace : ILuaElement
    {
        private readonly int level;
        private readonly string name = string.Empty;
        private readonly List<NameSpacePart> parts = new List<NameSpacePart>();
        private readonly Dictionary<string, NameSpace> subNamespaces = new Dictionary<string, NameSpace>();

        public NameSpace(NameSpacePart firstPart, int level)
        {
            this.name = firstPart.FullName[level - 1];
            this.level = level;
            this.AddPart(firstPart);
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.level == 1)
            {
                WriteHeaders(textWriter);
            }

            textWriter.WriteLine("{0} = {{", this.name);
            textWriter.Indent++;
            foreach (NameSpacePart part in this.parts)
            {
                part.WriteLua(textWriter, providers);
            }

            providers.PartialElementRegistry.WriteLua(textWriter, providers);

            foreach (var subPair in this.subNamespaces)
            {
                subPair.Value.WriteLua(textWriter, providers);
            }

            textWriter.Indent--;
            if (this.level == 1)
            {
                textWriter.WriteLine("};");
                this.WriteMainCallLua(textWriter, "_G");
            }
            else
            {
                textWriter.WriteLine("},");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new NotImplementedException();
        }

        public bool GotStaticClass()
        {
            return this.parts.Any(nsp => nsp.HasStaticClass()) ||
                   this.subNamespaces.Any(sns => sns.Value.GotStaticClass());
        }

        public void AddPart(NameSpacePart part)
        {
            if (part.FullName.IndexOf(part.Name) == this.level - 1)
            {
                this.parts.Add(part);
            }
            else
            {
                string subName = part.FullName[this.level];
                if (this.subNamespaces.ContainsKey(subName))
                {
                    this.subNamespaces[subName].AddPart(part);
                }
                else
                {
                    this.subNamespaces[subName] = new NameSpace(part, this.level + 1);
                }
            }
        }

        public void WriteMainCallLua(IndentedTextWriter writer, string nameSpace)
        {
            foreach (NameSpacePart part in this.parts)
            {
                part.WriteMainCallLua(writer, nameSpace + "." + this.name);
            }

            foreach (var subPair in this.subNamespaces)
            {
                subPair.Value.WriteMainCallLua(writer, nameSpace + "." + this.name);
            }
        }

        private static void WriteHeaders(IndentedTextWriter writer)
        {
            writer.WriteLine(LuaHeader.GetHeader());
        }
    }
}