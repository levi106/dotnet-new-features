using System;

namespace improved_definite_assignment_net5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            C c = new();
            if (c != null && c.M(out object obj1))
            {
                obj1.ToString();
            }

            if ((c != null && c.M(out object obj2)) == true)
            {
                obj2.ToString();
            }

            if ((c != null && c.M(out object obj3)) is true)
            {
                obj3.ToString();
            }
        }
    }

    public class C
    {
        public bool M(out object obj)
        {
            obj = new object();
            return true;
        }
    }
}
