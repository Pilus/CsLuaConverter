namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CsToLua.SyntaxAnalysis;
    using Microsoft.CodeAnalysis;

    public class RegistryBasedNameProvider : INameAndTypeProvider
    {
        private readonly LoadedNamespace rootNamespace;
        private List<LoadedNamespace> refenrecedNamespaces;
        private List<string> generics; 

        public RegistryBasedNameProvider(Solution solution)
        {
            this.rootNamespace = new LoadedNamespace(null);
            this.LoadSystemTypes();
            this.LoadSolution(solution);
        }

        private void LoadSystemTypes()
        {
            this.LoadType(typeof(Action));
            this.LoadType(typeof(Func<int>));
        }

        private void LoadSolution(Solution solution)
        {
            foreach (var project in solution.Projects)
            {
                this.LoadAssembly(Assembly.LoadFrom(project.OutputFilePath));
            }
        }

        private void LoadAssembly(Assembly assembly)
        {
            foreach(var type in assembly.GetTypes().Where(t => !t.Name.StartsWith("<")))
            {
                this.LoadType(type);
            }
        }

        private void LoadType(Type type)
        {
            var nameParts = type.FullName.Split('.');

            if (nameParts.Length < 2)
            {
                throw new NameProviderException(string.Format("Type name does not have any namespace: {0}", type.FullName));
            }

            LoadedNamespace currentNamespace = null;
            foreach (var namePart in nameParts.Take(nameParts.Length - 1))
            {
                currentNamespace = (currentNamespace ?? this.rootNamespace).Upsert(namePart);
            }

            if (currentNamespace == null)
            {
                throw new NameProviderException("No namespace found.");
            }
            currentNamespace.Upsert(type);
        }

        public void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces)
        {
            var baseRefencedNamespaces = new List<LoadedNamespace>()
            {
                this.rootNamespace,
            };

            var currentNamespaceNames = currentNamespace.Split('.');
            for (var i = 1; i <= currentNamespaceNames.Count(); i++)
            {
                baseRefencedNamespaces.Add(this.rootNamespace.TryGetNamespace(currentNamespaceNames.Take(i).ToList()));
            }

            this.refenrecedNamespaces = baseRefencedNamespaces.Where(x => true).ToList();

            foreach (var ns in namespaces)
            {
                var found = false;
                foreach (var refenrecedNamespace in baseRefencedNamespaces)
                {
                    var loadedNamespace = refenrecedNamespace.TryGetNamespace(ns.Split('.').ToList());
                    if (loadedNamespace != null)
                    {
                        this.refenrecedNamespaces.Add(loadedNamespace);
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    throw new NameProviderException(String.Format("Could not find namespace: {0}.", ns));
                }
            }
        }

        private List<ScopeElement> currentScope = new List<ScopeElement>();

        public List<ScopeElement> CloneScope()
        {
            return this.currentScope;
        }

        public void SetScope(List<ScopeElement> scope)
        {
            this.currentScope = scope;
        }

        public void AddToScope(ScopeElement element)
        {
            this.currentScope.Add(element);
        }

        public string LookupVariableName(IEnumerable<string> names)
        {
            var firstName = names.First();
            var variable = this.currentScope.FirstOrDefault(element => element.Name.Equals(firstName));
            if (variable != null)
            {
                return variable.Name;
            }

            foreach (var ns in this.refenrecedNamespaces)
            {
                if (ns.Types.ContainsKey(firstName))
                {
                    var type = ns.Types[firstName];
                    var additionalName = string.Empty;
                    if (names.Count() > 1)
                    {
                        additionalName = "." + string.Join(".", names.Skip(1).ToArray());
                    }
                    return type.Type.FullName + additionalName;
                }
            }

            throw new NameProviderException(string.Format("Could not find a variable for {0}", firstName));
        }

        public string LoopupFullNameOfType(string name)
        {
            return StripGenerics(this.LookupType(new []{name}).Type.FullName);
        }

        public TypeResult LookupType(IEnumerable<string> names)
        {
            var firstName = StripGenerics(names.First());
            foreach (var refenrecedNamespace in this.refenrecedNamespaces)
            {
                if (refenrecedNamespace.Types.ContainsKey(firstName))
                {
                    return refenrecedNamespace.Types[firstName].GetTypeResult();
                }
            }

            throw new System.NotImplementedException();
        }

        private static string StripGenerics(string name)
        {
            return name.Split('`').First();
        }

        public void AddMembersToScope(Type type)
        {
            throw new System.NotImplementedException();
        }

        public void AddAllInheritedMembersToScope(Type type)
        {
            //throw new System.NotImplementedException();
        }

        public string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference)
        {
            return StripGenerics(this.LookupType(names).Type.FullName);
        }

        public string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference, bool chooseTypeName)
        {
            return StripGenerics(this.LookupType(names).Type.FullName);
        }


        private readonly Dictionary<string, string> defaultValues = new Dictionary<string, string>
        {
            {"bool", "false"},
            {"int", "0"},
            {"float", "0"},
            {"long", "0"},
            {"double", "0"},
            {"string", "\"\""},
        };

        private readonly List<string> typesWithNoDefaultValue = new List<string>
        {
            "Action",
            "object",
            "Func",
        };

        public string GetDefaultValue(string typeName, bool isNullable)
        {
            return isNullable ? "nil" : this.GetDefaultValue(typeName);
        }

        public string GetDefaultValue(string typeName)
        {
            var typeNameWithoutGenerics = StripGenerics(typeName);
            if (this.defaultValues.ContainsKey(typeNameWithoutGenerics))
            {
                return this.defaultValues[typeNameWithoutGenerics];
            }

            if (typeName.StartsWith("Array<"))
            {
                return "nil";
            }

            if (this.typesWithNoDefaultValue.Contains(typeNameWithoutGenerics))
            {
                return "nil";
            }

            try
            {
                var type = this.LookupType(typeName.Split('.'));
                if (type.Type.IsEnum)
                {
                    var values = Enum.GetValues(type.Type);
                    foreach (var value in values)
                    {
                        return "'" + value + "'";
                    }
                }
            }
            catch (TypeLookupException)
            {

            }

            return "nil";
        }

        public void SetGenerics(IEnumerable<string> generics)
        {
            this.generics = generics.ToList();
        }

        public bool IsGeneric(string name)
        {
            return this.generics != null && this.generics.Contains(name);
        }
    }
}