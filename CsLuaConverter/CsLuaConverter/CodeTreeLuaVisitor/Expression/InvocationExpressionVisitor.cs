namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CodeTree;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;

    public class InvocationExpressionVisitor : BaseVisitor
    {
        private static readonly string[] namespacesWithNoAmbigiousMethods = new [] {"Lua"};
        private readonly BaseVisitor target;
        private readonly ArgumentListVisitor argumentList;
        public InvocationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var syntax = (InvocationExpressionSyntax)this.Branch.SyntaxNode;
            var symbol = (IMethodSymbol)providers.SemanticModel.GetSymbolInfo(syntax).Symbol;

            if (symbol.IsExtensionMethod && symbol.MethodKind == MethodKind.ReducedExtension)
            {
                this.WriteAsExtensionMethodCall(textWriter, providers, symbol);
                return;
            }

            textWriter.Write("(");

            if (symbol.MethodKind != MethodKind.DelegateInvoke)
            {
                var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
                var signatureHasGenerics = providers.SignatureWriter.WriteSignature(symbol.ConstructedFrom.Parameters.Select(p => p.Type).ToArray(), signatureTextWriter);

                if (signatureHasGenerics)
                {
                    var targetVisitor = textWriter.CreateTextWriterAtSameIndent();
                    this.VisitTarget(targetVisitor, providers, symbol);

                    var expectedEnd = $".{symbol.Name}.";
                    if (targetVisitor.ToString().EndsWith(expectedEnd))
                    {
                        throw new Exception($"Expect index visitor to end with '{expectedEnd}'. Got '{targetVisitor}'");
                    }

                    var targetString = targetVisitor.ToString();
                    textWriter.Write(targetString.Remove(targetString.Length - expectedEnd.Length + 1));
                    textWriter.Write($"['{symbol.Name}");
                }
                else
                {
                    this.VisitTarget(textWriter, providers, symbol);
                }

                var fullNamespace = providers.SemanticAdaptor.GetFullNamespace(symbol.ContainingType);
                if (!namespacesWithNoAmbigiousMethods.Contains(fullNamespace))
                { 
                    textWriter.Write("_M_{0}_", symbol.TypeArguments.Length);

                    if (signatureHasGenerics)
                    {
                        textWriter.Write("'..(");
                    }

                    textWriter.AppendTextWriter(signatureTextWriter);

                    if (signatureHasGenerics)
                    {
                        textWriter.Write(")]");
                    }
                }
            }
            else
            {
                this.target.Visit(textWriter, providers);
            }

            if (symbol.TypeArguments.Any())
            {
                providers.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            this.argumentList.Visit(textWriter, providers);
        }

        private void VisitTarget(IIndentedTextWriterWrapper textWriter, IProviders providers, IMethodSymbol symbol)
        {
            this.target.Visit(textWriter, providers);
        }

        private void WriteAsExtensionMethodCall(IIndentedTextWriterWrapper textWriter, IProviders providers,
            IMethodSymbol symbol)
        {
            textWriter.Write("(({0} % _M.DOT).", providers.SemanticAdaptor.GetFullName(symbol.ContainingType));

            var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
            var signatureHasGenerics = providers.SignatureWriter.WriteSignature(symbol.ReducedFrom.Parameters.Select(p => p.Type).ToArray(), signatureTextWriter);

            if (signatureHasGenerics)
            {
                textWriter.Write("['");
            }
            
            textWriter.Write("{0}_M_{1}_", symbol.Name, symbol.TypeArguments.Length);

            if (signatureHasGenerics)
            {
                textWriter.Write("'..(");
            }

            textWriter.AppendTextWriter(signatureTextWriter);

            if (signatureHasGenerics)
            {
                textWriter.Write(")]");
            }

            if (symbol.TypeArguments.Any())
            {
                providers.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            var argWriter = textWriter.CreateTextWriterAtSameIndent();
            this.argumentList.Visit(argWriter, providers);

            var targetWriter = textWriter.CreateTextWriterAtSameIndent();
            this.VisitTarget(targetWriter, providers, symbol);
            var targetStr = targetWriter.ToString();
            textWriter.Write(targetStr.Substring(0, targetStr.LastIndexOf(" % _M.DOT)")));

            var argStr = argWriter.ToString();
            if (argStr.Length > 2)
            {
                textWriter.Write(", ");
            }
            
            textWriter.Write(argStr.Substring(1)); // Skip the opening (
        }
    }
}