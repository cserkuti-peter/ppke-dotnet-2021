using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstClassLibrary
{
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
