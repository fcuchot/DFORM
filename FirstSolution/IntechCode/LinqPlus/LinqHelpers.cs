using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntechCode.LinqPlus
{
    public static class LinqHelpers
    {
        public static IEnumerable<int> EvenNumbers(int start, int count) 
                                        => Enumerable.Range(start, count)
                                                     .Select(n => n * 2);

        public static int Fibonacci(int n) => n < 2 ? n : Fibonacci(n - 2) + Fibonacci(n - 1);


        public static IEnumerable<int> Fibonacci()
        {
            int v = 0;
            int prev = 1;
            while(true)
            {
                int prevV = v;
                yield return v;
                v += prev;
                prev = prevV;
            }
        }

        public static IEnumerable<int> MyFavoriteNumbers()
        {
            yield return 3;
            yield return 33;
            yield return 39876;
            yield return 14;
            yield return 981237;
            yield return 3712;
        }

    }
}
