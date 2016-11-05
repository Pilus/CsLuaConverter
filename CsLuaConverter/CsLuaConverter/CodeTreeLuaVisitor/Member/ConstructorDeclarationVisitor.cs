namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using Expression;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;

    public class ConstructorDeclarationVisitor : BaseVisitor
    {
        private readonly Scope scope;
        private readonly ParameterListVisitor parameterList;
        private readonly BlockVisitor block;
        private readonly BaseConstructorInitializerVisitor baseConstructorInitializer;
        private readonly ThisConstructorInitializerVisitor thisConstructorInitializer;

        public ConstructorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.PublicKeyword, SyntaxKind.ProtectedKeyword);
            this.scope = (Scope)Enum.Parse(typeof(Scope), ((CodeTreeLeaf)this.Branch.Nodes[0]).Text, true);
            this.parameterList =
                (ParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.ParameterList)).Single();
            this.block =
                (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).Single();
            this.baseConstructorInitializer =
                (BaseConstructorInitializerVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.BaseConstructorInitializer)).SingleOrDefault();
            this.thisConstructorInitializer =
                (ThisConstructorInitializerVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ThisConstructorInitializer)).SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var syntax = (ConstructorDeclarationSyntax)this.Branch.SyntaxNode;
            var symbol = providers.SemanticModel.GetDeclaredSymbol(syntax);

            textWriter.WriteLine("_M.IM(members, '', {");
            textWriter.Indent++;

            //var scope = providers.NameProvider.CloneScope();

            //textWriter.Write("types = {");
            //this.parameterList.WriteAsTypes(textWriter, providers);
            //textWriter.WriteLine("},");

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Cstor',");
            textWriter.WriteLine("static = true,");
            textWriter.WriteLine("numMethodGenerics = 0,");
            textWriter.Write("signatureHash = ");
            providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            //this.parameterList.GetTypes(providers).WriteSignature(textWriter, providers);
            textWriter.WriteLine(",");
            textWriter.WriteLine("scope = '{0}',", this.scope);

            textWriter.Write("func = function(element");
            this.parameterList.FirstElementPrefix = ", ";
            this.parameterList.Visit(textWriter, providers);
            textWriter.WriteLine(")");

            textWriter.Indent++;
            if (this.baseConstructorInitializer != null)
            {
                this.baseConstructorInitializer.Visit(textWriter, providers);
            }
            else if (this.thisConstructorInitializer != null)
            {
                this.thisConstructorInitializer.Visit(textWriter, providers);
            }
            else
            {
                textWriter.WriteLine("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();");
            }

            textWriter.Indent--;

            this.block.Visit(textWriter, providers);

            textWriter.WriteLine("end,");
            
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public static void WriteEmptyConstructor(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("_M.IM(members, '', {");
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Cstor',");
            textWriter.WriteLine("static = true,");
            textWriter.WriteLine("numMethodGenerics = 0,");
            textWriter.WriteLine("signatureHash = 0,");
            textWriter.WriteLine("scope = 'Public',");
            textWriter.WriteLine("func = function(element)");

            textWriter.Indent++;
            textWriter.WriteLine("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();");
            textWriter.Indent--;

            textWriter.WriteLine("end,");

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }
    }
}