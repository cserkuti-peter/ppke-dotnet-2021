using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ReflectionExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFrom("MyFirstClassLibrary.dll");

            foreach (var t in assembly.GetTypes())
            {
                Console.WriteLine(t.Name);

                new List<MethodInfo>(t.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)).ForEach(m => Console.WriteLine(m.Name));
                new List<PropertyInfo>(t.GetProperties()).ForEach(m => Console.WriteLine(m.Name));
                new List<FieldInfo>(t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)).ForEach(m => Console.WriteLine(m.Name));
                new List<EventInfo>(t.GetEvents()).ForEach(m => Console.WriteLine(m.Name));
            }

            var r = new Room { RoomNumber = "202", Size = 30, IsSecretRoom = false };
            PrettyPrinter.Print(r);

        }
    }

    [DebuggerDisplay("Room: {RoomNumber}")]
    [PrettyPrintable]
    public class Room
    {
        [PrettyPrint]
        public string RoomNumber { get; set; }

        [PrettyPrint(Capitalize = true)]
        public int Size { get; set; }

        public bool IsSecretRoom { get; set; }
    }
}
