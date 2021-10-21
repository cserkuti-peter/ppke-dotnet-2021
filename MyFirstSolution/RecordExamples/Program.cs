using System;

namespace RecordExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = new PersonRecord("Peter", "Cserkuti");
            var r2 = new PersonRecord("Peter", "Cserkuti");
            var b = r1 == r2;
            Console.WriteLine($"{r1.GetHashCode()}, {r2.GetHashCode()}");
            Console.WriteLine(r1.ToString());

            var r3 = r1 with
            { 
                FirstName = "Gabor"
            };
        }
    }

    //  Fancy classes that are
    //  * ref types but work as value types
    //  * immutable (easy/safe sharing of data, thread-safe, read-only
    //  -----------
    //  * when to use it: multi-thread, safe-share, VM, DTO, data not changes
    //  * when not to use: when data changes (EF)
    public record PersonRecord(string FirstName, string SecondName);

    public record PersonRecord2(string FirstName, string SecondName)
    { 
        public string FullName 
        {
            get => $"{FirstName} {SecondName}";
        }

        public void SayHello()
        {
            Console.WriteLine($"Hello {FullName}");
        }
    }

    public class PersonClass
    {
        public string FirstName { get; init; }
        public string SecondName { get; init; }
    }
}
