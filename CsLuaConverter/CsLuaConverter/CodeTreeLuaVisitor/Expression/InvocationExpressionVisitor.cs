namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CodeTree;
    using CsLuaConverter.Context;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (InvocationExpressionSyntax)this.Branch.SyntaxNode;
            var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol;

            if (symbol.IsExtensionMethod && symbol.MethodKind == MethodKind.ReducedExtension)
            {
                this.WriteAsExtensionMethodCall(textWriter, context, symbol);
                return;
            }

            textWriter.Write("(");

            if (symbol.MethodKind != MethodKind.DelegateInvoke)
            {
                var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
                var signatureHasGenerics = context.SignatureWriter.WriteSignature(symbol.ConstructedFrom.Parameters.Select(p => p.Type).ToArray(), signatureTextWriter);

                if (signatureHasGenerics)
                {
                    var targetVisitor = textWriter.CreateTextWriterAtSameIndent();
                    this.VisitTarget(targetVisitor, context, symbol);

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
                    this.VisitTarget(textWriter, context, symbol);
                }

                var fullNamespace = context.SemanticAdaptor.GetFullNamespace(symbol.ContainingType);
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
                this.target.Visit(textWriter, context);
            }

            if (symbol.TypeArguments.Any())
            {
                context.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            this.argumentList.Visit(textWriter, context);
        }

        private void VisitTarget(IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            this.target.Visit(textWriter, context);
        }

        private void WriteAsExtensionMethodCall(IIndentedTextWriterWrapper textWriter, IContext context,
            IMethodSymbol symbol)
        {
            textWriter.Write("(({0} % _M.DOT).", context.SemanticAdaptor.GetFullName(symbol.ContainingType));

            var signatureTextWriter = textWriter.CreateTextWriterAtSameIndent();
            var signatureHasGenerics = context.SignatureWriter.WriteSignature(symbol.ReducedFrom.Parameters.Select(p => p.Type).ToArray(), signatureTextWriter);

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
                context.TypeReferenceWriter.WriteTypeReferences(symbol.TypeArguments.ToArray(), textWriter);
            }

            textWriter.Write(" % _M.DOT)");

            var argWriter = textWriter.CreateTextWriterAtSameIndent();
            this.argumentList.Visit(argWriter, context);

            var targetWriter = textWriter.CreateTextWriterAtSameIndent();
            this.VisitTarget(targetWriter, context, symbol);
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