namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class NamespaceVisitor : IVisitor<Namespace>, IVisitor<NamespaceElement>
    {
        public void Visit(Namespace element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("{0} = {{", element.Name);
            textWriter.Indent++;
            textWriter.WriteLine("__metaType = _M.MetaTypes.NameSpace,");

            element.Elements.Where(e => !(e.Element is ClassDeclaration)).ToList().ForEach(VisitorList.Visit);
            var classPairs = element.Elements.Where(e => e.Element is ClassDeclaration)
                .GroupBy(e => (e.Element as ClassDeclaration).Name);

            foreach (var pair in classPairs)
            {
                textWriter.Write("{0} = _M.NE({{", pair.Key);
                
                foreach (var classNamespaceElement in pair)
                {
                    VisitorList.Visit(classNamespaceElement);
                }

                textWriter.WriteLine("}),");
            }


            element.SubNamespaces.ForEach(VisitorList.Visit);

            textWriter.Indent--;
            textWriter.WriteLine("}" + (element.IsRoot ? "" : ","));

            WriteFooter(element, textWriter, providers);
        }

        public void Visit(NamespaceElement element, IndentedTextWriter textWriter, IProviders providers)
        {
            providers.TypeProvider.SetNamespaces(element.NamespaceLocation, this.CollectUsings(element.Usings));
            VisitorList.Visit(element.Element, textWriter, providers);
        }

        public IEnumerable<string> CollectUsings(List<UsingDirective> usings)
        {
            return usings.Select(this.GetFullNameOfUsing);
        }

        public string GetFullNameOfUsing(UsingDirective theUsing)
        {
            return string.Join(".", theUsing.Name.Names);
        }

        public static void WriteFooter(Namespace element, IndentedTextWriter textWriter, IProviders providers)
        {
            element.Elements.ForEach(e => WriteFooter(e, textWriter, providers));

            element.SubNamespaces.ForEach(e => WriteFooter(e, textWriter, providers));
        }

        public static void WriteFooter(NamespaceElement element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Element is ClassDeclaration)
            {
                ClassVisitor.WriteFooter((ClassDeclaration) element.Element, textWriter, providers, element.Attributes);
            }
        }
    }
}