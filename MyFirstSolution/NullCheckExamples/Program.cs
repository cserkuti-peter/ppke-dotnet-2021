using System;
using System.Collections.Generic;

namespace NullCheckExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //  null-coalasing operator
            //  ??
            int? a = null;
            int b = a ?? 0;
            //int b = a != null ? (int)a : 0;
            Console.WriteLine(b);

            //  null-coalasing assignment
            //  ??=
            int? a2 = null;
            a2 ??= 0;
            //a2 = a2 != null ? a2 : 0;

            List<int> numbers = null;
            (numbers ??= new List<int>()).Add(5);
            Console.WriteLine(String.Join(" ", numbers));

            //  Null-conditional
            //.? .[]
            Person p = null;
            Console.WriteLine(p?.Name ?? "N/A");

            Func<string> f1 = () => null;
            Console.WriteLine(f1?.Invoke() ?? "Nothing");
        }
    }

    public class Person
    {
        public string Name { get; private set; }

        public Person(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
