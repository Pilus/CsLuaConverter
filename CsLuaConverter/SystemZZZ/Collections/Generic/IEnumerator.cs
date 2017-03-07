namespace SystemZZZ.Collections.Generic
{
    public interface IEnumerator<T> : IDisposable, IEnumerator
    {
        T Current { get; }
    }
}