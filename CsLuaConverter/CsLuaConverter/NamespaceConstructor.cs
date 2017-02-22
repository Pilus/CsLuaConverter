namespace CsLuaConverter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.CodeTreeLuaVisitor.Elements;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class NamespaceConstructor
    {
        private readonly IContext context;

        public NamespaceConstructor(IContext context)
        {
            this.context = context;
        }

        public IEnumerable<Namespace> GetNamespacesFromProject(Project project)
        {
            IEnumerable<Document> documents = project.Documents
                .Where(doc => doc.Folders.FirstOrDefault() != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var documentContents = documents.Select(document => new DocumentContent(document));

            var allTypeMembers = documentContents.SelectMany(content => content.Syntax.GetBaseTypeMembers().Select(syntax => new NamespaceMember(content, syntax)));

            var membersByFullName = allTypeMembers.GroupBy(member => member.GetFullName()).OrderBy(pair => pair.Key).ToArray();

            var uniqueNamespaces = membersByFullName.Select(pair => pair.Key.Split('.').First()).Distinct().ToArray();

            return uniqueNamespaces.Select(namespaceName => new Namespace()
            {
                Name = namespaceName,
                WritingAction = (textWriter) =>
                {
                    var memberGroups = FilterNamespaceMember(membersByFullName, namespaceName).ToArray();
                    this.WriteNamespaceMemberGroups(memberGroups, textWriter);
                    this.WriteFooter(memberGroups.Select(g => g.First()), textWriter);
                }
            });
        }

        private static IEnumerable<IGrouping<string, NamespaceMember>> FilterNamespaceMember(IEnumerable<IGrouping<string, NamespaceMember>> pairs, string namespaceName)
        {
            return pairs.Where(pair => FilterNamespaceMember(pair, namespaceName));
        }

        private static bool FilterNamespaceMember(IGrouping<string, NamespaceMember> pair, string namespaceName)
        {
            return pair.Key.StartsWith(namespaceName + ".");
        }

        private void WriteNamespaceMemberGroups(IEnumerable<IGrouping<string, NamespaceMember>> namespaceMemberGroups, IIndentedTextWriterWrapper textWriter)
        {
            foreach (var namespaceMemberGroup in namespaceMemberGroups.OrderBy(group => group.First().GetNamespaceName()))
            {
                this.WriteNamespaceMembers(namespaceMemberGroup, textWriter);
            }
        }

        private void WriteNamespaceMembers(IGrouping<string, NamespaceMember> namespaceMemberGroup, IIndentedTextWriterWrapper textWriter)
        {
            var groupByNumGenerics = namespaceMemberGroup.ToArray().GroupBy(member => member.GetNumGenerics()).OrderBy(group => group.Key);

            textWriter.WriteLine($"_M.ATN('{namespaceMemberGroup.First().GetNamespaceName()}','{namespaceMemberGroup.First().GetName()}', _M.NE({{");
            textWriter.Indent++;

            foreach (var namespaceMemberGroupWithSameNumGenerics in groupByNumGenerics)
            {
                var state = this.context.PartialElementState;
                state.CurrentState = null;
                state.DefinedConstructorWritten = false;

                state.NumberOfGenerics = namespaceMemberGroupWithSameNumGenerics.Key;
                var members = namespaceMemberGroupWithSameNumGenerics.ToArray();
                while (true)
                {
                    for (int index = 0; index < members.Length; index++)
                    {
                        state.IsFirst = index == 0;
                        state.IsLast = index == members.Length - 1;

                        this.WriteNamespaceMember(members[index], textWriter);
                    }

                    if (state.NextState == null)
                    {
                        break;
                    }

                    state.CurrentState = state.NextState;
                }
            }

            textWriter.Indent--;
            textWriter.WriteLine("}));");
        }

        private void WriteNamespaceMember(NamespaceMember namespaceMember, IIndentedTextWriterWrapper textWriter)
        {
            BaseVisitor.TryActionAndWrapException(
                () =>
                {
                    this.context.SemanticModel = namespaceMember.SemanticModel;
                    namespaceMember.Syntax.Write(textWriter, this.context);
                },
                $"In document {namespaceMember.Path}");
        }

        private void WriteFooter(IEnumerable<NamespaceMember> members, IIndentedTextWriterWrapper textWriter)
        {
            foreach(var classMember in members.Where(member => member.Syntax is ClassDeclarationSyntax))
            {
                this.context.SemanticModel = classMember.SemanticModel;
                this.context.PartialElementState.CurrentState = (int)ClassState.Footer;
                classMember.Syntax.Write(textWriter, this.context);
            }
        }
    }
}