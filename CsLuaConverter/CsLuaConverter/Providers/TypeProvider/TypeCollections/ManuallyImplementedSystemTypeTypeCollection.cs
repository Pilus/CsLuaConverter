namespace CsLuaConverter.Providers.TypeProvider.TypeCollections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using CsLuaFramework;
    using CsLuaFramework.Wrapping;

    public class ManuallyImplementedSystemTypeTypeCollection : BaseTypeCollection
    {
        public ManuallyImplementedSystemTypeTypeCollection()
        {
            this.LoadSystemTypes();
        }

        private void LoadSystemTypes()
        {
            this.Add(typeof(Type));
            this.Add(typeof(Action));
            this.Add(typeof(Action<>));
            this.Add(typeof(Action<,>));
            this.Add(typeof(Action<,,>));
            this.Add(typeof(Action<,,,>));
            this.Add(typeof(Action<,,,,>));
            this.Add(typeof(Action<,,,,,>));
            this.Add(typeof(Action<,,,,,,>));
            this.Add(typeof(Action<,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,,,,,>));
            this.Add(typeof(Action<,,,,,,,,,,,,,,,>));
            this.Add(typeof(Func<>));
            this.Add(typeof(Func<,>));
            this.Add(typeof(Func<,,>));
            this.Add(typeof(Func<,,,>));
            this.Add(typeof(Func<,,,,>));
            this.Add(typeof(Func<,,,,,>));
            this.Add(typeof(Func<,,,,,,>));
            this.Add(typeof(Func<,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,,,,,>));
            this.Add(typeof(Func<,,,,,,,,,,,,,,,>));
            this.Add(typeof(Tuple<>));
            this.Add(typeof(Tuple<,>));
            this.Add(typeof(Tuple<,,>));
            this.Add(typeof(Tuple<,,,>));
            this.Add(typeof(Tuple<,,,,>));
            this.Add(typeof(Tuple<,,,,,>));
            this.Add(typeof(Tuple<,,,,,,>));
            this.Add(typeof(Tuple<,,,,,,,>));
            this.Add(typeof(Exception));
            this.Add(typeof(NotImplementedException));
            this.Add(typeof(ArgumentOutOfRangeException));
            this.Add(typeof(Enum));
            this.Add(typeof(ICsLuaAddOn));
            this.Add(typeof(IDictionary));
            this.Add(typeof(IDictionary<,>));
            this.Add(typeof(Dictionary<,>));
            this.Add(typeof(KeyValuePair<,>));
            this.Add(typeof(IReadOnlyDictionary<,>));
            this.Add(typeof(IList));
            this.Add(typeof(IList<>));
            this.Add(typeof(ICollection));
            this.Add(typeof(ICollection<>));
            this.Add(typeof(IEnumerable));
            this.Add(typeof(IEnumerable<>));
            this.Add(typeof(IReadOnlyList<>));
            this.Add(typeof(IReadOnlyCollection<>));
            this.Add(typeof(List<>));
            this.Add(typeof(Enumerable));
            this.Add(typeof(Array));
            this.Add(typeof(IMultipleValues<>));
            this.Add(typeof(IMultipleValues<,>));
            this.Add(typeof(IMultipleValues<,,>));
            this.Add(typeof(IMultipleValues<,,,>));
            this.Add(typeof(IMultipleValues<,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,,,,,>));
            this.Add(typeof(IMultipleValues<,,,,,,,,,,,>));
            this.Add(typeof(Enumerable));
            this.Add(typeof(Activator));
            this.Add(typeof(Guid));
        }
    }
}