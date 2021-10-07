using System;

namespace PropertiesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var p = new Person
            {
                FirstName = "Peter",
                SecondName = "Cserkuti"
            };
            Console.WriteLine(p.FullName);
        }
    }

    public class Person
    {
        private string firstName;

        public string FirstName
        {
            get => firstName;
            set => firstName = !String.IsNullOrEmpty(value) ? value : throw new ArgumentException(nameof(FirstName));
            //{
            //    if (String.IsNullOrEmpty(value))
            //        throw new ArgumentException(nameof(FirstName));

            //    firstName = value; 
            //}
        }

        public string SecondName { get; set; }

        public string FullName
        {
            get => $"{FirstName} {SecondName}";
        }
    }
}
