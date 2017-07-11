using System;


namespace IntechCode
{
    public class HelloWorld
    {
        public static string SayHelloWorld( int count )
        {
#if DEBUG
            Console.WriteLine("Hello world!");
#else
            Console.WriteLine("Hello world! (in Release!)");
#endif
            return $"Count is {count:X}";
        }

    }
}
