using System;
using System.Threading;

namespace SimpleAppForDocker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now);
                Thread.Sleep(1000);
            }
        }
    }
}
