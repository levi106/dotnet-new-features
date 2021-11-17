using System;

namespace interpolated_string_handler_net5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "Frodo";
            int age = 51;
            string message = $"{name}: {age}";
            Console.WriteLine(message);

            object ageObj = age;
            string message2 = string.Format("{0}: {1}", name, ageObj);
            Console.WriteLine(message2);

            Console.WriteLine($"{1} {2} {3}");
            Console.WriteLine($"{1} {2} {3} {4}");
            Console.WriteLine($"{1} {2} {3} {4} {5}");

            string ageString = age.ToString();
            Console.WriteLine(ageString);
        }
    }
}
