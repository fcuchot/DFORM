using FluentAssertions;
using IntechCode.IntechCollection;
using IntechCode.LinqPlus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace IntechCode.Tests
{
    [TestFixture]
    public class MyLinkTests
    {
        [Test]
        [Fact]
        public void MyLinq_supports_Where()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(4);
            list.Add(7);
            list.Add(-17);
            list.Add(10);

            int x = 98;
            bool isOdd = (x & 1) != 0;

            Func<int, bool> filter = num => (num & 1) != 0;

            var allOdds = list.Where(filter);
            var allPositiveOdds = allOdds.Where( i => i >= 0 );

            allPositiveOdds.Count().Should().Be(2);

        }

        [Test]
        [Fact]
        public void Fibo()
        {
            int num = 0;
            foreach (var i in LinqHelpers.Fibonacci().Take(20))
            {
                i.Should().Be(LinqHelpers.Fibonacci(num));
                ++num;
            }
        }

        [TestCase(40)]
        public void Fibo_recursive_performance( int n )
        {
            Console.WriteLine("---");
            Console.WriteLine($"FibIterable({n}) = {LinqHelpers.Fibonacci().Skip(n).First()}");
            Console.WriteLine("---");
            Console.WriteLine($"FibRecurse({n}) = {LinqHelpers.Fibonacci(n)}");
            Console.WriteLine("---");
        }


        [Test]
        [Fact]
        public void SelectYTest()
        {
            var l = new MyList<int>();
            l.Add(1);
            l.Add(23);
            l.Add(234);
            l.Add(98);
            l.Add(78676);
            l.Add(3333);
            l.Add(2121);
            int num = 0;
            foreach (string s in l.YSelect(e => e.ToString()))
            {
                s.Should().Be(l[num].ToString());
                num++;
            }
        }

        // Test

        [Test]
        [Fact]
        public void MyList_implements_select()
        {
            var l = new MyList<int>();
            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.Add(4);
            l.Add(5);
            l.Add(6);

            int num = 0;
            foreach (string s in l.Select(e => e.ToString()))
            {
                s.Should().Be(l[num].ToString());
                num++;
            }
        }

    }

    static class MyLinqDeFou
    {
        class En<T> : IMyEnumerable<T>
        {
            readonly IMyEnumerable<T> _source;
            readonly Func<T, bool> _predicate;

            public En(IMyEnumerable<T> s, Func<T, bool> p)
            {
                _source = s;
                _predicate = p;
            }

            class E : IMyEnumerator<T>
            {
                readonly IMyEnumerator<T> _source;
                readonly Func<T,bool> _predicate;

                public E(En<T> e)
                {
                    _source = e._source.GetEnumerator();
                    _predicate = e._predicate;
                }

                public T Current => _source.Current;

                public bool MoveNext()
                {
                    while(_source.MoveNext())
                    {
                        if (_predicate(_source.Current)) return true;
                    }
                    return false;
                }
            }

            public IMyEnumerator<T> GetEnumerator() => new E(this);
        }

        public static IMyEnumerable<T> Where<T>(this IMyEnumerable<T> @this, Func<T, bool> predicate)
        {
            return new En<T>(@this, predicate);
        }

        class EnSelect<T, G> : IMyEnumerable<G>
        {
            readonly IMyEnumerable<T> _source;
            readonly Func<T, G> _map;

            public EnSelect(IMyEnumerable<T> s, Func<T, G> p)
            {
                _source = s;
                _map = p;
            }

            class E : IMyEnumerator<G>
            {
                readonly IMyEnumerator<T> _source;
                readonly Func<T, G> _map;

                public E(EnSelect<T, G> e)
                {
                    _source = e._source.GetEnumerator();
                    _map = e._map;
                }

                public G Current => _map(_source.Current);

                public bool MoveNext()
                {
                    while (_source.MoveNext())
                    {
                        return true;
                    }
                    return false;
                }
            }

            public IMyEnumerator<G> GetEnumerator() => new E(this);
        }


        // Select

        public static IMyEnumerable<G> Select<T, G>(this IMyEnumerable<T> @this, Func<T, G> map)
        {
            return new EnSelect<T, G>(@this, map);
        }



        public static IEnumerable<G> YSelect<T, G>(this IMyEnumerable<T> @this, Func<T, G> map)
        {
            foreach (T t in @this)
            {
                yield return map(t);
            }
        }


        public static IEnumerable<T> YWhere<T>(this IMyEnumerable<T> @this, Func<T, bool> predicate)
        {
            foreach (var e in @this)
                if (predicate(e)) yield return e;
        }

        public static int Count<T>(this IMyEnumerable<T> @this )
        {
            int i = 0;
            foreach (var e in @this) ++i;
            return i;
        }


    }


}
