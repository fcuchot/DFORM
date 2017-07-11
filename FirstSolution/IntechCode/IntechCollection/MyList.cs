using System;

namespace IntechCode.IntechCollection
{
    public class MyList<T> : IMyList<T>
    {
        T[] _items;
        int _count;

        public MyList()
        {
            _items = new T[16];
        }

        public T this[int index] 
        {
            get
            {
                if (index >= _count) throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index >= _count) throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        public int Count => _count;

        public void Add(T item) => Insert(_count, item);

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count) throw new IndexOutOfRangeException();
            if (_count == _items.Length)
            {
                // Realloc needed.
                T[] newItems = new T[ _items.Length * 2];
                Array.Copy(_items, 0, newItems, 0, _count);
                _items = newItems;
            }
            if (index == _count) _items[_count] = item;
            else
            {
                Array.Copy(_items, index, _items, index + 1, _count - index);
                _items[index] = item;
            }
            ++_count;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            Array.Copy(_items, index+1, _items, index, _count - index -1);
            _items[--_count] = default(T);
        }

        public int IndexOf(T item)
        {
            for( int i = 0; i < _count; ++i)
            {
                if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(item, _items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        class E : IMyEnumerator<T>
        {
            readonly MyList<T> _list;
            int _currentIndex;

            public E( MyList<T> theList )
            {
                _list = theList;
                _currentIndex = -1;
            }

            public T Current => _list._items[_currentIndex];

            public bool MoveNext()
            {
                return ++_currentIndex < _list._count;
            }
        }

        public IMyEnumerator<T> GetEnumerator()
        {
            return new E( this );
        }

    }
}
