using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPLExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var limit = 2_000_000;
            var numbers = Enumerable.Range(1, limit).ToList();

            //  Task Parallel Library (TPL)
            int count = 0;
            var bag = new ConcurrentBag<int>();
            //  1. For
            //Parallel.For(1, limit,
            //    index =>
            //    {
            //        var n = numbers[index];
            //        if (IsPrime(n))
            //        {
            //            Interlocked.Increment(ref count);
            //            bag.Add(n);
            //        }
            //    });

            //  2. Foreach
            //Parallel.ForEach(numbers,
            //    n =>
            //    {
            //        if (IsPrime(n))
            //        {
            //            Interlocked.Increment(ref count);
            //            bag.Add(n);
            //        }
            //    });

            //  3. Using thread-local variable
            //  count
            Parallel.ForEach(
                numbers,
                () => 0,
                (n, loop, localSum) =>
                {
                    if (IsPrime(n))
                    {
                        localSum++;
                    }
                    return localSum;
                },
                finalResult => Interlocked.Add(ref count, finalResult)
            );

            //  set
            var set = new HashSet<int>();
            var syncRoot = new object();
            Parallel.ForEach(
                numbers,
                () => new HashSet<int>(),
                (n, loop, localSet) =>
                {
                    if (IsPrime(n))
                    {
                        localSet.Add(n);
                    }
                    return localSet;
                },
                finalResult =>
                {
                    lock (syncRoot)
                    {
                        set.UnionWith(finalResult);
                    }
                }
            );

            //  PLINQ, parallel LINQ
            var source = Enumerable.Range(1, 10000);
            var evenNumbers = from num in source.AsParallel()
                              where num % 2 == 0
                              select num;
        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
