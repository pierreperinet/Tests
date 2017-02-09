using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            StartThread1();
            StartThread2();
            Console.ReadKey();
        }

        private static void StartThread1()
        {
            Thread t1 = new Thread(delegate ()
            {
                System.Console.Write("Hello, ");
                Thread.Sleep(1000);
                System.Console.WriteLine("World!");
            });
            t1.Start();
        }

        private static void StartThread2()
        {
            Thread t1 = new Thread(() =>
            {
                System.Console.Write("Hello, ");
                Thread.Sleep(1000);
                System.Console.WriteLine("World!");
            });
            t1.Start();
        }
    }
}
