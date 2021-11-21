using System;

namespace lambda_expression_improvements_net5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var choose = (bool b) => b ? 1 : "two"; // ERROR: CS8773
            // var choose = object (bool b) => b ? 1 : "two"; // ERROR: CS8773
            Func<bool, object> choose = (bool b) => b ? 1 : "two";
            choose(true);
        }
    }
}
