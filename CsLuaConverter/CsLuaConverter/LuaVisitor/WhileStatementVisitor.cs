﻿namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class WhileStatementVisitor : IVisitor<WhileStatement>
    {
        public void Visit(WhileStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("while (");
            VisitorList.Visit(element.Statement);
            textWriter.WriteLine(") do");
            VisitorList.Visit(element.Block);
            textWriter.WriteLine("end");
        }
    }
}