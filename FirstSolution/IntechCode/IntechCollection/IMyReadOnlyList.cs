using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public interface IMyReadOnlyList<out T> : IMyReadOnlyCollection<T>
    {
        T this[int index] { get; }

    }
}
