#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;

namespace default_constraint_net5
{
    class A2
    {
        public virtual void F2<T>(T? t) where T : struct { Console.WriteLine("A2.F2 struct"); }
        public virtual void F2<T>(T? t) { Console.WriteLine("A2.F2"); }
    }

    class B2 : A2
    {
        public override void F2<T>(T? t) /*where T : struct*/ { Console.WriteLine("B2.F2"); }
        public override void F2<T>(T? t) where T : default { Console.WriteLine("B2.F2 default"); }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            B2 b2 = new B2();
            b2.F2<int>(19);
            b2.F2<string>("abc");
        }
    }
}
