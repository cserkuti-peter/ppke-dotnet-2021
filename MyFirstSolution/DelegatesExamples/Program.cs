using System;
using System.Linq.Expressions;

namespace DelegatesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> dlgt = WriteSomething;
            dlgt += delegate (string message)
            {
                Console.WriteLine("Message from anonymous function: " + message);
            };
            dlgt += message =>
            {
                Console.WriteLine("Message from lambda expression: " + message);
                Console.WriteLine("Message from lambda expression2: " + message);
            };

            dlgt("Hello");

            Func<int, int> f = x => x * x;
            int r = f(2);
            Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine(e);
        }

        public static void WriteSomething(string message)
        {
            Console.WriteLine("Message from method: " + message);
        }

        //public delegate void WriteSomethingDlgt(string message);
    }


}
