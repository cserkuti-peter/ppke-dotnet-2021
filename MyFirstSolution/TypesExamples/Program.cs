using System;

namespace TypesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Value type vs reference types
            var p = new Point();
            p.X = 1;
            p.Y = 2;

            var p2 = p;
            p2.X = 100;

            Console.WriteLine($"{p.X} {p.Y}");
            Console.WriteLine($"{p2.X} {p2.Y}");

            //  Enums
            Color c = Color.Red;

            var meetingDays = Days.Monday | Days.Tuesday;

            var isMeetingOnTuesday = (meetingDays & Days.Tuesday) == Days.Tuesday;

            //  Nullable types
            Nullable<int> i = null;
            int? j = 1;
            Console.WriteLine(i.HasValue ? i.Value : 0);

            //  Tuples
            (double, int) t1 = (1.2, 2);
            var t2 = (2.2, "Hello");
            var t3 = (Sum: 10, Avg: 3);

            var mm1 = FindMinMax(new int[] { 1, 3, -9});
            var (min, max) = FindMinMax(new int[] { 1, 3, -9 });

            //  Array
            int[] a1 = new int[5];
            int[] a2 = new int[] { 1, 2, 3};
            int[] a3 = { 1, 2, 3 };
            int[,] r = new int[2, 3];
            int[][] jagged = new int[6][];
            jagged[0] = new int[2];
            jagged[1] = new int[3];

            for (int x = 0; x < a2.Length; x++)
            {
                Console.WriteLine(a2[x]);
            }

            foreach (var x in a2)
            {
                Console.WriteLine(x);
            }

            //  Object
            int y = 1;
            object o = y;       //  boxing
            int y2 = (int)o;    //  unboxing
        }

        private static (int min, int max) FindMinMax(int[] input)
        {
            if (input is null || input.Length == 0)
            {
                throw new ArgumentException("Cannot find minimum and maximum of a null or empty array.");
            }

            var min = int.MaxValue;
            var max = int.MinValue;
            foreach (var i in input)
            {
                if (i < min)
                {
                    min = i;
                }
                if (i > max)
                {
                    max = i;
                }
            }
            return (min, max);
        }
    }

    public class Point
    {
        public int X;
        public int Y;
    }

    public enum Color
    { 
        Red,
        Green,
        Blue
    }

    [Flags]
    public enum Days
    {
        None = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        Weekend = Saturday | Sunday
    }



}
