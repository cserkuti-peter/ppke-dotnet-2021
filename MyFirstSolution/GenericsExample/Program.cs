using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Generic: class, interface, delegate, method
            //  ArrayList
            //  List, Dictionary, Queue, Stack, HashSet, ...
            var list = new List<Person>
            {
                new Person { Name = "Peter" },
                new Person { Name = "David" }
            };
            //p.Add(new Person { Name = "Peter" });
            //p.Add(new Person { Name = "David" });

            foreach (var p in list)
            {
                Console.WriteLine(p);
            }

            list.ForEach(p => Console.WriteLine(p));

            var list2 = list.FindAll(p => p.Name.StartsWith("P"));

            list.Sort((p1, p2) => p1.Id - p2.Id);
            list.Sort((p1, p2) => String.Compare(p1.Name, p2.Name));

            var p1 = new Person { Name = "Peter" };
            var p2 = new Person { Name = "David" };
            var dict = new Dictionary<int, Person>();
            dict.Add(p1.Id, p1);
            dict.Add(p2.Id, p2);

            var dict2 = list.ToDictionary(p => p.Id);
            foreach (var item in dict2)
            {
                Console.WriteLine($"{item.Key}, {item.Value}");
            }
            var p3 = dict[1];
        }
    }

    public class Person
    {
        private static int nextId = 1;
        public int Id { get; private set; }

        public Person()
        {
            Id = nextId++;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
