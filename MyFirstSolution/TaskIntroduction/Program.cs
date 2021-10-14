using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskIntroduction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //  Orinary threads
            //new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Console.Write('x');
            //    }
            //}).Start();

            //ThreadPool.QueueUserWorkItem((state) => 
            //{
            //    while (true)
            //    {
            //        Console.Write('z');
            //    }
            //});

            //while (true)
            //{
            //    Console.Write('y');
            //}

            var t1 = new Task(() =>
            {
                Console.WriteLine("Doing...");
                Thread.Sleep(3000);
                Console.WriteLine("Im done.");
            });

            t1.Start();
            Console.WriteLine("Doing somthing in the main thread");

            //t1.Wait();  //  blocking
            await t1;   //  non-blockg

            var t2 = new Task<int>(() =>
            {
                Console.WriteLine("Doing2...");
                Thread.Sleep(3000);
                Console.WriteLine("Im done2.");
                return 100;
            });

            t2.Start();
            Console.WriteLine("Doing somthing in the main thread2");

            //t2.Wait();
            //var result = t2.Result;  //  blocking
            var result2 = await t2;

            var result = await Task<int>.Run(() =>
            {
                Console.WriteLine("Doing2...");
                Thread.Sleep(3000);
                Console.WriteLine("Im done2.");
                return 100;
            });
            Console.WriteLine("Doing somthing in the main thread3");

            using (var httpClient = new HttpClient())
            {
                var respont = await httpClient.GetAsync("https://www.encosoftware.hu/");
            }
        }
    }
}
