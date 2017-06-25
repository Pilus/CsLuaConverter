namespace SystemZZZ.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    public class Iterator<T> : IEnumerable, IEnumerable<T>
    {
        private readonly IEnumerator<T> enumerator;

        public Iterator(Action enumerator)
        {
            this.enumerator = enumerator as IEnumerator<T>; // Only works in CsLua
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.enumerator;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.enumerator;
        }
    }
}