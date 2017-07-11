using System;

namespace IntechCode.IntechCollection
{
    public interface IMyList<T> : IMyReadOnlyList<T>
    {
        void Add( T item );

        void RemoveAt( int index );

        void Insert( int index, T item );

        int IndexOf( T item );

        new T this[ int index ] { get; set; }
    }

}
