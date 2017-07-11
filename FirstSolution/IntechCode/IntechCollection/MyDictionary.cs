using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IntechCode.IntechCollection
{
    public class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>
    {
         static readonly int[] _primes = {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

        class Node
        {
            public KeyValuePair<TKey, TValue> Data;
            public Node Next;
        }
        Node[] _buckets;
        int _count;
        readonly Func<TKey, TKey, bool> _equality;
        readonly Func<TKey, int> _hash;

        public MyDictionary(Func<TKey, TKey, bool> equality = null, Func<TKey, int> hash = null)
        {
            _equality = equality ?? EqualityComparer<TKey>.Default.Equals;
            _hash = hash ?? EqualityComparer<TKey>.Default.GetHashCode;
            _buckets = new Node[7];
        }

        public MyDictionary(IEqualityComparer<TKey> comparer)
            : this( comparer == null ? (Func<TKey, TKey, bool>)null : comparer.Equals, 
                    comparer == null ? (Func<TKey,int >)null :  comparer.GetHashCode)
        {
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue found;
                if (TryGetValue(key, out found)) return found;
                throw new KeyNotFoundException();
            }
            set => DoAdd(key, value, true);
        }

        public int Count => _count;

        public void Add(TKey key, TValue value)
        {
            DoAdd(key, value, false);
        }

        void DoAdd(TKey key, TValue value, bool allowUpdate)
        {
            int idxBucket = Math.Abs(_hash(key)) % _buckets.Length;
            Node head = _buckets[idxBucket];
            Node found;
            if (head != null && (found = FindIn(head, key)) != null)
            {
                if(allowUpdate)
                {
                    found.Data = new KeyValuePair<TKey, TValue>(found.Data.Key, value);
                    return;
                }
                throw new Exception("Duplicate key.");
            }
            _buckets[idxBucket] = new Node()
            {
                Data = new KeyValuePair<TKey, TValue>(key, value),
                Next = head
            };
            ++_count;
        }

        Node FindIn(Node head, TKey key)
        {
            Debug.Assert(head != null);
            do
            {
                if (_equality(key, head.Data.Key) ) break;
                head = head.Next;
            }
            while (head != null);
            return head;
        }

        public bool ContainsKey(TKey key)
        {
            int idxBucket = Math.Abs(_hash(key)) % _buckets.Length;
            Node head = _buckets[idxBucket];
            return head != null ? FindIn(head, key) != null : false;
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int idxBucket = Math.Abs(_hash(key)) % _buckets.Length;
            Node n = _buckets[idxBucket];
            if (n == null || (n = FindIn(n, key)) == null)
            {
                value = default(TValue);
                return false;
            }
            value = n.Data.Value;
            return true;
        }

        class E : IMyEnumerator<KeyValuePair<TKey,TValue>>
        {
            readonly MyDictionary<TKey,TValue> _dictionnary;
            Node _currentNode;
            int _currentIndex;

            public E(MyDictionary<TKey, TValue> theDict)
            {
                _dictionnary = theDict;
                _currentIndex = -1;
            }

            public KeyValuePair<TKey, TValue> Current  => _currentNode.Data;

            public bool MoveNext()
            {
                if( _currentNode != null && _currentNode.Next != null)
                {
                    _currentNode = _currentNode.Next;
                    return true;
                }
                while (++_currentIndex < _dictionnary._buckets.Length)
                {
                    if (_dictionnary._buckets[_currentIndex] != null)
                    {
                        _currentNode = _dictionnary._buckets[_currentIndex];
                        return true;
                    }
                }
                return false;
            }
        }

        public IMyEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new E(this);
        }

    }
}
