using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public interface IMyEnumerator<out T>
    {
        T Current { get; }

        bool MoveNext();
    }
}
