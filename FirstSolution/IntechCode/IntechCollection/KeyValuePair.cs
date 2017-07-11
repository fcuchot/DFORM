using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public struct KeyValuePair<TKey,TValue>
    {
        public readonly TKey Key;
        public readonly TValue Value;

        public KeyValuePair( TKey k, TValue v )
        {
            Key = k;
            Value = v;
        }
    }
}
