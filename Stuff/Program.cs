using System;
using System.Linq;

namespace Stuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Fib(20));
            Console.WriteLine(Fib3(100));
            Console.WriteLine(Fib2(100));
            Console.ReadKey();
            SelectFunc();
            Console.ReadKey();
        }

        private static void SelectFunc()
        {
            int[] values = { 3, 7, 10 };
            Func<int, string> projection = x => "Value=" + x;
            var strings = values.Select(projection);

            foreach (string s in strings)
            {
                Console.WriteLine(s);
            }


            int[] fibNum = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            var averageValueSelect = fibNum.Select(num => "-" + num);
            foreach (var x in averageValueSelect)
            {
                Console.WriteLine(x);
            }

            var averageValue = fibNum.Where(num => num % 2 == 1).Average();
            Console.WriteLine(averageValue);
            Console.ReadLine();



            int[] source = new[] { 3, 8, 4, 6, 1, 7, 9, 2, 4, 8 };

            foreach (int i in source.Where(
                x =>
                {
                    if (x <= 3)
                        return true;
                    else if (x >= 7)
                        return true;
                    return false;
                }))
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }

        private static int Fib(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            return Fib(n - 1) + Fib(n - 2);
        }

        private static long Fib3(long n)
        {
            /* Declare an array to store fibonacci numbers. */
            long[] f = new long[n + 1];

            /* 0th and 1st number of the series are 0 and 1*/
            f[0] = 0;
            f[1] = 1;

            for (var i = 2; i <= n; i++)
            {
                /* Add the previous 2 numbers in the series and store it */
                f[i] = f[i - 1] + f[i - 2];
            }

            return f[n];
        }

        private static long Fib2(long n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            long f = 0;
            long f0 = 0;
            long f1 = 1;
            for (var i = 2; i <= n; i++)
            {
                f = f1 + f0;
                f0 = f1;
                f1 = f;
            }
            return f;
        }
    }
}
