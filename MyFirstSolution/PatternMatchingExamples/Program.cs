using System;
using System.Drawing;

namespace PatternMatchingExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Fruit fruit = new Apple { Color = Color.Red };

            //if (fruit.GetType() == typeof(Apple) || fruit.Color == Color.Green)
            if (fruit is Apple && fruit.Color == Color.Green)
            {
                var apple = fruit as Apple;
            }

            //  Pattern matching with switch statement
            switch (fruit)
            {
                case Apple apple when apple.Color == Color.Green:
                    apple.MakeApplePieFrom();
                    break;
                case Apple apple when apple.Color == Color.Brown:
                    apple.ThrowAway();
                    break;
                case Orange orange:
                    orange.Peel();
                    break;
                default:
                    break;
            }

            //  Switch expression
            var text = fruit switch
            { 
                Apple { IsRipe: true } => "This is a ripe apple",
                { IsRipe: true } => "This a ripe fruit",
                _ => "This is not a ripe apple"
            };

            //  Tupple pattern
            var state = State.Open;
            var action = Action.Close;

            var newState = (state, action) switch
            {
                (State.Open, Action.Close) => State.Closed,
                (State.Open, Action.Open) => throw new InvalidOperationException("Cannot open."),
                (State.Closed, Action.Open) => State.Open,
                (State.Closed, Action.Close) => throw new InvalidOperationException("Cannot close."),,
            };
        }

        public enum State { Open, Closed }
        public enum Action { Open, Close }
    }

    public class Fruit
    {
        public Color Color { get; set; }
        public bool IsRipe { get; set; }
    }

    public enum AppleType
    {
        Golden,
        PinkLady,
        Gala
    }

    public class Apple : Fruit
    {
        public AppleType AppleType { get; set; }
        public void MakeApplePieFrom() { Console.WriteLine("Making applepie from the apple."); }
        public void ThrowAway() { Console.WriteLine("Throwing away the apple."); }
    }

    public class Orange : Fruit
    {
        public void Peel() { Console.WriteLine("Peeling the orange."); }
    }

}
