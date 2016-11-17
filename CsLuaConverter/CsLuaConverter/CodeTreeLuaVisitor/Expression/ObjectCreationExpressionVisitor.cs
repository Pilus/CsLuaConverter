namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Collections.Generic;
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
        private readonly ArgumentListVisitor constructorArgumentsVisitor;
        private readonly IVisitor initializer;

        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);

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


            ITypeSymbol[] parameterTypes = null;
            IDictionary<ITypeSymbol, ITypeSymbol> appliedClassGenerics = null;
            if (symbol != null)
            {
                providers.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);
                parameterTypes = symbol.OriginalDefinition.Parameters.Select(p => p.Type).ToArray();
                appliedClassGenerics = ((TypeSymbolSemanticAdaptor) providers.SemanticAdaptor).GetAppliedClassGenerics(symbol.ContainingType);
            }
            else
            {
                // Special case for missing symbol. Roslyn issue 3825. https://github.com/dotnet/roslyn/issues/3825
                var namedTypeSymbol = (INamedTypeSymbol)providers.SemanticModel.GetSymbolInfo(node.Type).Symbol;
                providers.TypeReferenceWriter.WriteInteractionElementReference(namedTypeSymbol, textWriter);

                if (namedTypeSymbol.TypeKind != TypeKind.Delegate)
                {
                    throw new Exception($"Could not guess constructor for {namedTypeSymbol}.");
                }

                parameterTypes = new ITypeSymbol[] { namedTypeSymbol };
            }

            var signatureWiter = textWriter.CreateTextWriterAtSameIndent();
            var hasGenricComponents = providers.SignatureWriter.WriteSignature(parameterTypes, signatureWiter, appliedClassGenerics);

            if (hasGenricComponents)
            {
                textWriter.Write("['_C_0_'..");
            }
            else
            {
                textWriter.Write("._C_0_");
            }

            textWriter.AppendTextWriter(signatureWiter);

            if (hasGenricComponents)
            {
                textWriter.Write("]");
            }
            
            this.constructorArgumentsVisitor.Visit(textWriter, providers);

            if (this.initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                this.initializer.Visit(textWriter, providers);
            }
        }
    }
}