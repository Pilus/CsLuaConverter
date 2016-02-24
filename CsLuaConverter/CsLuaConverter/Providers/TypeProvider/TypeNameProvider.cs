namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsLuaFramework;
    using Microsoft.CodeAnalysis;

    public class TypeNameProvider : ITypeProvider
    {
        private readonly LoadedNamespace rootNamespace;
        private List<LoadedNamespace> refenrecedNamespaces;

        private List<NativeTypeResult> predefinedNativeTypeResults = new List<NativeTypeResult>()
        {
            new NativeTypeResult("int", typeof(int)),
            new NativeTypeResult("object", typeof(object)),
            new NativeTypeResult("string", typeof(string)),
            new NativeTypeResult("bool", typeof(bool)),
            new NativeTypeResult("long", typeof(long)),
            new NativeTypeResult("double", typeof(double)),
            new NativeTypeResult("float", typeof(float)),
            new NativeTypeResult("void", typeof(void)),
        };

        public TypeNameProvider(Solution solution)
        {
            this.rootNamespace = new LoadedNamespace(null);
            this.LoadSystemTypes();
            this.LoadAssembly(Assembly.Load("Lua"));
            this.LoadAssembly(Assembly.Load("CsLuaFramework"));
            this.LoadSolution(solution);
        }

        private void LoadSystemTypes()
        {
            this.LoadType(typeof(Type));
            this.LoadType(typeof(Action));
            this.LoadType(typeof(Func<int>));
            this.LoadType(typeof(Exception));
            this.LoadType(typeof(NotImplementedException));
            this.LoadType(typeof(Enum));
            this.LoadType(typeof(ICsLuaAddOn));
            this.LoadType(typeof(IDictionary));
            this.LoadType(typeof(IDictionary<object, object>));
            this.LoadType(typeof(Dictionary<object, object>));
            this.LoadType(typeof(IList));
            this.LoadType(typeof(IList<object>));
            this.LoadType(typeof(ICollection));
            this.LoadType(typeof(ICollection<object>));
            this.LoadType(typeof(IEnumerable));
            this.LoadType(typeof(IEnumerable<object>));
            this.LoadType(typeof(IReadOnlyList<object>));
            this.LoadType(typeof(IReadOnlyCollection<object>));
            this.LoadType(typeof(List<object>));
            this.LoadType(typeof(Enumerable));
        }

        private void LoadSolution(Solution solution)
        {
            foreach (var project in solution.Projects)
            {
                try {
                    this.LoadAssembly(Assembly.LoadFrom(project.OutputFilePath));
                }
                catch (FileNotFoundException)
                {
                    throw new ConverterException(string.Format("Could not find the file {0}. Please build or rebuild the {1} project.", project.OutputFilePath, project.Name));
                }
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
            var nameParts = StripGenerics(type.FullName).Split('.');

            if (nameParts.Length < 2)
            {
                return;
            }

            LoadedNamespace currentNamespace = null;
            foreach (var namePart in nameParts.Take(nameParts.Length - 1))
            {
                currentNamespace = (currentNamespace ?? this.rootNamespace).Upsert(namePart);
            }

            if (currentNamespace == null)
            {
                throw new ProviderException("No namespace found.");
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
            for (var i = currentNamespaceNames.Count(); i >= 1; i--)
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
                    throw new ProviderException($"Could not find namespace: {ns}.");
                }
            }
        }

        public ITypeResult LookupType(string name)
        {
            var nativeType = this.predefinedNativeTypeResults.FirstOrDefault(t => name.Equals(t.NativeName));
            if (nativeType != null)
            {
                return nativeType;
            }

            var nameWithoutGenerics = StripGenerics(name);
            foreach (var refenrecedNamespace in this.refenrecedNamespaces)
            {
                if (refenrecedNamespace.Types.ContainsKey(nameWithoutGenerics))
                {
                    return refenrecedNamespace.Types[nameWithoutGenerics].GetTypeResult();
                }
            }
            throw new ProviderException(string.Format("Could not find type '{0}' in the referenced namespaces.", name));
        }

        public ITypeResult LookupType(IEnumerable<string> names)
        {
            if (names.Count() == 1)
            {
                return this.LookupType(names.Single());
            }

            var nameWithoutGenerics = StripGenerics(names.First());
            foreach (var refenrecedNamespace in this.refenrecedNamespaces)
            {
                if (refenrecedNamespace.Types.ContainsKey(nameWithoutGenerics))
                {
                    return refenrecedNamespace.Types[nameWithoutGenerics].GetTypeResult(string.Join(".", names.Skip(1)));
                }
                
                if (names.Count() > 1 && refenrecedNamespace.SubNamespaces.ContainsKey(nameWithoutGenerics))
                {
                    var current = refenrecedNamespace.SubNamespaces[nameWithoutGenerics];
                    var remainingNames = names.Skip(1);
                    
                    while (remainingNames.Any())
                    {
                        var name = StripGenerics(remainingNames.First());
                        remainingNames = remainingNames.Skip(1);

                        if (current.Types.ContainsKey(name))
                        {
                            return current.Types[name].GetTypeResult(string.Join(".", remainingNames));
                        }
                        else if (current.SubNamespaces.ContainsKey(name))
                        {
                            current = current.SubNamespaces[name];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
          
            throw new ProviderException(string.Format("Could not find type '{0}' in the referenced namespaces.", string.Join(".", names)));
        }


        private static string StripGenerics(string name)
        {
            return name.Split('`').First();
        }


        

    }
}