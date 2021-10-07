using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionExamples
{
    public static class PrettyPrinter
    {
        public static void Print(object obj)
        {
            var classAttr = Attribute.GetCustomAttribute(obj.GetType(), typeof(PrettyPrintableAttribute));

            if (classAttr == null)
                throw new ArgumentException("The type is not pretty printable.");

            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                var propAttr = (PrettyPrintAttribute)Attribute.GetCustomAttribute(p, typeof(PrettyPrintAttribute));

                if (propAttr != null)
                {
                    var propValue = p.GetValue(obj).ToString();

                    if (propAttr.Capitalize)
                        Console.WriteLine($"{p.Name}: {propValue}".ToUpper());
                    else
                        Console.WriteLine($"{p.Name}: {propValue}");
                }
            }
        }
    }
}
