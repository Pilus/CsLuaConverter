namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Providers.TypeProvider;

    public class FullNameProvider
    {
        private static readonly string[] AllowedSystemTypes = {"object", "int", "double", "string", "bool", "Action", "Func", "Array"};
        
        private readonly List<Type> loadedTypes;
        private string currentNamespace;

        private List<ScopeElement> knownInCurrentScope = new List<ScopeElement>();
        private List<string> usedNamespaces;
        private List<Type> usedTypes;


        public FullNameProvider(List<Assembly> assemblies)
        {
            this.loadedTypes = new List<Type>();
            assemblies.ForEach(a => this.loadedTypes.AddRange(a.GetTypes().Where(t => !t.Name.StartsWith("<>"))));
        }

        private static IEnumerable<string> GetAllNameCombinations(string name)
        {
            string[] names = name.Split('.');
            var allCombinations = new List<string>();
            for (int i = 1; i <= names.Count(); i++)
            {
                allCombinations.Add(string.Join(".", names.Take(i)));
            }
            return allCombinations;
        }

        public void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces)
        {
            this.currentNamespace = currentNamespace;
            var usedNamespacesAndParentSpaces = new List<string>();
            IEnumerable<string> parentNamespaces = GetAllNameCombinations(currentNamespace);
            usedNamespacesAndParentSpaces.AddRange(parentNamespaces);
            foreach (string ns in namespaces)
            {
                if (this.loadedTypes.Any(t => t.FullName.StartsWith(ns)))
                {
                    usedNamespacesAndParentSpaces.Add(ns);
                }

                foreach (string parentNamespace in parentNamespaces)
                {
                    if (this.loadedTypes.Any(t => t.FullName.StartsWith(parentNamespace + "." + ns)))
                    {
                        usedNamespacesAndParentSpaces.Add(parentNamespace + "." + ns);
                    }
                }
            }
            this.usedNamespaces = usedNamespacesAndParentSpaces.Distinct().ToList();

            this.usedTypes = this.loadedTypes.Where(t => t.Namespace != null && (
                t.Namespace.Equals(this.currentNamespace) ||
                this.usedNamespaces.Any(ns => t.Namespace.Equals(ns)))).ToList();
        }

        public List<ScopeElement> CloneScope()
        {
            return this.knownInCurrentScope.Where(x => true).ToList();
        }

        public void SetScope(List<ScopeElement> scope)
        {
            this.knownInCurrentScope = scope;
        }

        public void AddToScope(ScopeElement element)
        {
            this.knownInCurrentScope.Add(element);
        }

        public string LoopupFullNameOfType(string name)
        {
            return this.LoopupFullNameOfType(new[] {name}, false);
        }


        private static string StripGenericsFromType(string typeMame)
        {
            return typeMame.Split('`').First();
        }

        private static int FullNameMatchesNames(string fullName, IEnumerable<string> names)
        {
            names = names.ToList();
            for (int i = 1; i <= names.Count(); i++)
            {
                if (StripGenericsFromType(fullName).EndsWith("." + string.Join(".", names.Take(i))))
                {
                    return i;
                }
                if (fullName.EndsWith("." + string.Join(".", names.Take(i))))
                {
                    return i;
                }
            }

            return 0;
        }

        public TypeResult LookupType(IEnumerable<string> names)
        {
            // Direct references
            for (int i = names.Count(); i > 0; i--)
            {
                Type directReference =
                    this.loadedTypes.SingleOrDefault(t => t.FullName.Equals(string.Join(".", names.Take(i))));
                if (directReference != null)
                {
                    return new TypeResult {Type = directReference, AdditionalString = string.Join(".", names.Skip(i))};
                }
            }

            List<Type> matches = this.usedTypes.Where(t => FullNameMatchesNames(t.FullName, names) > 0).ToList();
            if (matches.Count == 1)
            {
                Type match = matches.First();
                int matchAt = FullNameMatchesNames(match.FullName, names);
                return new TypeResult {Type = match, AdditionalString = string.Join(".", names.Skip(matchAt))};
            }

            string possibleName = this.currentNamespace + "." + names.First();
            Type typeInSameNamespace = matches.FirstOrDefault(m => m.FullName.Equals(possibleName));
            if (typeInSameNamespace != null)
            {
                return new TypeResult {Type = typeInSameNamespace};
            }

            throw new TypeLookupException("Name not found in assemblies. " + string.Join(".", names));
        }

        private IEnumerable<MemberInfo> GetMethodsOfType(Type type)
        {
            List<MethodInfo> publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();

            List<MethodInfo> privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            publicMethods.AddRange(privateMethods);
            List<MethodInfo> staticPublicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
            publicMethods.AddRange(staticPublicMethods);
            List<MethodInfo> staticPrivateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static).ToList();
            publicMethods.AddRange(staticPrivateMethods);

            return publicMethods;
        }

        public void AddMembersToScope(Type type)
        {
            IEnumerable<MemberInfo> methods = this.GetMethodsOfType(type);
            IEnumerable<MemberInfo> objectMethods = this.GetMethodsOfType(typeof(object));
            foreach (MemberInfo method in methods.Where(m => !objectMethods.Any(om => om.Name.Equals(m.Name))))
            {
                this.AddToScope(new ScopeElement(method.Name)
                {
                    ClassPrefix = "class.",
                    IsFromClass = true,
                });
            }
            foreach (MemberInfo member in type.GetMembers()) // Variables and properties
            {
                if (!member.MemberType.Equals(MemberTypes.Method) && !member.MemberType.Equals(MemberTypes.Constructor))
                {
                    this.AddToScope(new ScopeElement(member.Name)
                    {
                        ClassPrefix = "class.",
                        IsFromClass = true
                    });
                }
            }

            foreach (var property in type.GetRuntimeProperties())
            {
                this.AddToScope(new ScopeElement(property.Name)
                {
                    ClassPrefix = "class.",
                    IsFromClass = true
                });
            }

            foreach (var field in type.GetRuntimeFields())
            {
                if (!field.Name.EndsWith("__BackingField"))
                {
                    this.AddToScope(new ScopeElement(field.Name)
                    {
                        ClassPrefix = "class.",
                        IsFromClass = true
                    });
                }
            }
        }

        public void AddAllInheritedMembersToScope(Type type)
        {
            this.AddMembersToScope(type);
            if (type.BaseType.FullName != "System.Object")
            {
                this.AddAllInheritedMembersToScope(type.BaseType);
            }
        }


        public string LookupVariableName(IEnumerable<string> names)
        {
            return this.LoopupFullNameOfType(names, true);
        }

        public string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference)
        {
            return this.LoopupFullNameOfType(names, chooseClassReference, false);
        }

        public string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference, bool chooseTypeName)
        {
            var fullName = string.Join(".", names);
            string firstName = names.First();
            if (chooseClassReference && this.knownInCurrentScope.Any(e => e.Name.Equals(firstName) && e.IsFromClass))
                return this.knownInCurrentScope.First(e => e.Name.Equals(firstName) && e.IsFromClass).ClassPrefix +
                       fullName;

            if (!chooseTypeName && this.knownInCurrentScope.Any(e => e.Name.Equals(firstName) && !e.IsFromClass))
            {
                ScopeElement element =
                    this.knownInCurrentScope.FirstOrDefault(e => e.Name.Equals(firstName) && !e.IsFromClass);
                return element.ClassPrefix + fullName;
            }

            if (!chooseTypeName && this.knownInCurrentScope.Any(e => e.Name.Equals(firstName)))
            {
                ScopeElement element = this.knownInCurrentScope.FirstOrDefault(e => e.Name.Equals(firstName));
                return element.ClassPrefix + fullName;
                ;
            }

            if (firstName.Equals("List`1"))
            {
                return "CsLua.Collection.CsLuaList";
            }
            if (firstName.Equals("Dictionary`2"))
            {
                return "CsLua.Collection.CsLuaDictionary";
            }
            if (firstName.Equals("Global"))
            {
                return fullName;
            }
            if (firstName.Equals("NotImplementedException") || fullName == "System.NotImplementedException")
            {
                return "CsLua.NotImplementedException";
            }
            if (firstName.StartsWith("Tuple`"))
            {
                return "CsLua.Tuple";
            }
            if (fullName == "Enum.Parse")
            {
                return "__EnumParse";
            }

            var firstWithoutGenerics = firstName.Split('<').First().Split('`').First();
            if (AllowedSystemTypes.Contains(firstWithoutGenerics))
            {
                return firstWithoutGenerics;
            }

            TypeResult result = this.LookupType(names);
            return result.ToString();
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
            var typeNameWithoutGenerics = StripGenericsFromType(typeName);
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
            throw new NotImplementedException();
        }

        public bool IsGeneric(string name)
        {
            throw new NotImplementedException();
        }
    }
}