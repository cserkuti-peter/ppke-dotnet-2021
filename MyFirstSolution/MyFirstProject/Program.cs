using Figgle;
using MyFirstClassLibrary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyFirstProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "Hello world! My name is Peter Cserkúti.";

            var words = Helper.GetWords(s);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine(FiggleFonts.Acrobatic.Render("Hello world!"));
        }
    }
}
