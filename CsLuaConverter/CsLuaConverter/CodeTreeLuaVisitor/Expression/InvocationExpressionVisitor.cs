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
            textWriter.Write("(");
            var originalMethods = providers.Context.PossibleMethods;
            providers.Context.PossibleMethods = null;

            this.target.Visit(textWriter, providers);

            if (providers.Context.PossibleMethods != null)
            {
                var argumentListWriter = textWriter.CreateTextWriterAtSameIndent();
                this.argumentList.Visit(argumentListWriter, providers);

                var method = providers.Context.PossibleMethods.GetOnlyRemainingMethodOrThrow();

                var numGenerics = method.GetNumberOfMethodGenerics();

                if (!method.IsGetType())
                {
                    textWriter.Write($"_M_{numGenerics}_");

                    method.WriteSignature(textWriter, providers);

                    var writeMethodGenericsAction = providers.Context.PossibleMethods.WriteMethodGenerics;
                    if (writeMethodGenericsAction != null)
                    {
                        writeMethodGenericsAction();
                    }
                    else if (numGenerics > 0)
                    {
                        WriteMethodGenerics(method.GetResolvedMethodGenerics(), textWriter, providers);
                    }
                }

                textWriter.Write(" % _M.DOT)");

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

                textWriter.Write(" % _M.DOT)");
                this.argumentList.Visit(textWriter, providers);

                providers.Context.CurrentType = method.GetReturnType();
            }

            providers.Context.PossibleMethods = originalMethods;
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