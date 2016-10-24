namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;

    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;
    using Type;

    public class ObjectCreationExpressionVisitor : BaseVisitor
    {
        private readonly ITypeVisitor objectTypeVisitor;
        private readonly ArgumentListVisitor constructorArgumentsVisitor;
        private readonly IVisitor initializer;

        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);
            this.objectTypeVisitor = (ITypeVisitor) this.CreateVisitor(1);

            var i = 2;
            if (this.IsKind(i, SyntaxKind.ArgumentList))
            {
                this.constructorArgumentsVisitor = (ArgumentListVisitor)this.CreateVisitor(i);
                i++;
            }            

            if (this.IsKind(i, SyntaxKind.CollectionInitializerExpression) || this.IsKind(i, SyntaxKind.ObjectInitializerExpression))
            {
                this.initializer = this.CreateVisitor(i);
            }
            else if (this.Branch.Nodes.Length > i)
            {
                throw new VisitorException($"Unknown following argument to object creation: {branch.Nodes[3].Kind}.");
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var node = (ObjectCreationExpressionSyntax)this.Branch.SyntaxNode;
            var symbol = (IMethodSymbol)providers.SemanticModel.GetSymbolInfo(node).Symbol;

            textWriter.Write(this.initializer != null ? "(" : "");


            if (symbol == null)
            {
                var namedTypeSymbol = (INamedTypeSymbol)providers.SemanticModel.GetSymbolInfo(node.Type).Symbol;
                providers.TypeReferenceWriter.WriteInteractionElementReference(namedTypeSymbol, textWriter);
                textWriter.Write("._C_0_");

                throw new NotImplementedException();
            }

            

            providers.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);

            textWriter.Write($"._C_{symbol.TypeArguments.Length}_");

            providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            this.constructorArgumentsVisitor.Visit(textWriter, providers);

            if (this.initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                this.initializer.Visit(textWriter, providers);
            }

            /*
            //this.objectTypeVisitor.WriteAsReference(textWriter, providers);
            //var type = this.objectTypeVisitor.GetType(providers);


            if (this.constructorArgumentsVisitor != null)
            { 
                //providers.Context.PossibleMethods = new PossibleMethods(type.GetConstructors());

                var cstorArgsWriter = textWriter.CreateTextWriterAtSameIndent();
                this.constructorArgumentsVisitor.Visit(cstorArgsWriter, providers);

                var method = providers.Context.PossibleMethods.GetOnlyRemainingMethodOrThrow();
            
                method.WriteSignature(textWriter, providers);

                textWriter.AppendTextWriter(cstorArgsWriter);
            }
            else
            {
                textWriter.Write("0()");    
            }

            providers.Context.PossibleMethods = null;
            providers.Context.CurrentType = null;

            if (this.initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                //providers.Context.CurrentType = type;
                this.initializer.Visit(textWriter, providers);
            }
            

            //providers.Context.CurrentType = type;
            */
        }
    }
}