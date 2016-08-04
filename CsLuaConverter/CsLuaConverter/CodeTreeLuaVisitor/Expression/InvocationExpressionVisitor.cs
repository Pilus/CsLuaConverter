namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CodeTree;
    using Lists;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class InvocationExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        private readonly ArgumentListVisitor argumentList;
        public InvocationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            
            var originalMethods = providers.Context.PossibleMethods;
            providers.Context.PossibleMethods = null;

            var targetWriter = textWriter.CreateTextWriterAtSameIndent();
            this.target.Visit(targetWriter, providers);

            if (providers.Context.PossibleMethods != null)
            {
                var argumentListWriter = textWriter.CreateTextWriterAtSameIndent();
                this.argumentList.Visit(argumentListWriter, providers);

                var method = providers.Context.PossibleMethods.GetOnlyRemainingMethodOrThrow();

                var numGenerics = method.GetNumberOfMethodGenerics();

                

                if (!method.IsGetType())
                {
                    var signatureWriter = textWriter.CreateTextWriterAtSameIndent();
                    var signatureHasGenericComponents = method.WriteSignature(signatureWriter, providers);

                    textWriter.Write("(");

                    var targetString = targetWriter.ToString();
                    var targetElements = SplitByLastDot(targetString);
                    textWriter.Write(targetElements[0]);
                    textWriter.Write(signatureHasGenericComponents ? "['" : ".");
                    textWriter.Write(targetElements[1]);
                    textWriter.Write($"_M_{numGenerics}_" + (signatureHasGenericComponents ? "'.." : ""));
                    textWriter.AppendTextWriter(signatureWriter);

                    var writeMethodGenericsAction = providers.Context.PossibleMethods.WriteMethodGenerics;
                    if (writeMethodGenericsAction != null)
                    {
                        writeMethodGenericsAction(textWriter);
                    }
                    else if (numGenerics > 0)
                    {
                        WriteMethodGenerics(method.GetResolvedMethodGenerics(), textWriter, providers);
                    }
                    textWriter.Write((signatureHasGenericComponents ? "]" : "") + " % _M.DOT)");
                }
                else
                {
                    textWriter.Write("(");
                    textWriter.AppendTextWriter(targetWriter);
                    textWriter.Write(" % _M.DOT)");
                }

                textWriter.AppendTextWriter(argumentListWriter);

                providers.Context.CurrentType = method.GetReturnType();
            }
            else
            {
                var currentType = providers.Context.CurrentType;

                if (!currentType.IsDelegate())
                {
                    throw new VisitorException("Cannot invoke non delegate.");
                }

                var invoke = currentType.GetTypeObject().GetMember("Invoke").OfType<MethodBase>().First();
                var method = new MethodKnowledge(invoke);
                providers.Context.PossibleMethods = new PossibleMethods(new []{ method });
                providers.Context.CurrentType = null;

                textWriter.Write("(");
                textWriter.AppendTextWriter(targetWriter);
                textWriter.Write(" % _M.DOT)");
                this.argumentList.Visit(textWriter, providers);

                providers.Context.CurrentType = method.GetReturnType();
            }

            providers.Context.PossibleMethods = originalMethods;
        }

        private static string[] SplitByLastDot(string str)
        {
            var split = str.Split('.');
            var first = string.Join(".", split.Take(split.Length - 1));
            return new[] {first, split[split.Length - 1]};
        }

        private static void WriteMethodGenerics(TypeKnowledge[] genericTypes, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("[{");
            var first = true;
            genericTypes.ToList().ForEach(genericType =>
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    textWriter.Write(", ");
                }

                genericType.WriteAsType(textWriter, providers);
            });
            textWriter.Write("}]");
        }
    }
}