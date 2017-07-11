using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public interface IMyReadOnlyCollection<out T> : IMyEnumerable<T>
    {
        /// <summary>
        /// Gets the count of items that exist in this collection.
        /// </summary>
        int Count { get; }
    }
}
