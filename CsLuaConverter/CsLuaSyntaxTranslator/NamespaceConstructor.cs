namespace CsLuaSyntaxTranslator
{
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.MSBuild;
    public class NamespaceConstructor
    {
        private readonly IContext context;

        public NamespaceConstructor(IContext context)
        {
            this.context = context;
        }

        public NamespaceConstructor()
        {
            this.context = new Context.Context();
        }

        public IEnumerable<Namespace> GetNamespacesFromProject(string projectPath)
        {
            var workspace = MSBuildWorkspace.Create();

            var task = workspace.OpenProjectAsync(projectPath);
            task.Wait();
            return this.GetNamespacesFromProject(task.Result);
        }

        public IEnumerable<Namespace> GetNamespacesFromProject(Project project)
        {
            IEnumerable<Document> documents = project.Documents
                .Where(doc => Enumerable.FirstOrDefault<string>(doc.Folders) != "Properties"
                              && !doc.FilePath.EndsWith("AssemblyAttributes.cs")
                );

            var documentContents = documents.Select(document => new DocumentContent(document));

            var allTypeMembers = documentContents.SelectMany(content => MemberExtensions.GetBaseTypeMembers(content.Syntax).Select(syntax => new NamespaceMember(content, syntax)));

            var membersByFullName = allTypeMembers.GroupBy(member => member.GetFullName()).OrderBy(pair => pair.Key).ToArray();

            var uniqueNamespaces = GetCommonNamespaces(membersByFullName.Select(memberGroup => memberGroup.Key).ToArray());

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

        private static string[] GetCommonNamespaces(string[] fullMemberNames)
        {
            var commonNamespaces = new List<string[]>();
            foreach (var fullMemberName in fullMemberNames)
            {
                var namespaceSplit = fullMemberName.Split('.');
                var found = false;
                for (var index = 0; index < commonNamespaces.Count; index++)
                {
                    var commonNamespace = commonNamespaces[index];
                    var matchCount = MatchCount(commonNamespace, namespaceSplit);
                    if (matchCount >= commonNamespace.Length)
                    {
                        found = true;
                        break;
                    }
                    else if (matchCount > 0)
                    {
                        commonNamespaces[index] = commonNamespace.Take(matchCount).ToArray();
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    commonNamespaces.Add(namespaceSplit.Take(fullMemberName.Length - 1).ToArray());
                }
            }
            
            return commonNamespaces.Select(str => string.Join(".", str)).ToArray();
        }

        private static int MatchCount(string[] a, string[] b)
        {
            var index = 0;
            while (index < a.Length && index < b.Length && a[index] == b[index])
            {
                index++;
            }

            return index;
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
            WrappingException.TryActionAndWrapException(
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