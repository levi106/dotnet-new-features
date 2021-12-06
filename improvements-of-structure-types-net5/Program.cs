﻿using System;

namespace improvements_of_structure_types_net5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m1 = new Measurement();
            Console.WriteLine(m1);

            var m2 = default(Measurement);
            Console.WriteLine(m2);

            var ms = new Measurement[2];
            Console.WriteLine(string.Join(", ", ms));

            //var m3 = new Measurement(5);
            //Console.WriteLine(m3);
        }
    }

    public readonly struct Measurement
    {
        // ERROR
        //public Measurement()
        //{
        //    Value = double.NaN;
        //    Description = "Undefined";
        //}

        //public Measurement(double value)
        //{
        //    Value = value;
        //}

        public Measurement(double value, string description)
        {
            Value = value;
            Description = description;
        }

        public double Value { get; init; }
        // ERROR
        //public string Description { get; init; } = "Ordinary measurement";
        public string Description { get; init; }

        public override string ToString() => $"{Value} ({Description})";
    }
}
