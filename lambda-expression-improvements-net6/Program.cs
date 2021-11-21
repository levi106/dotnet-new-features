// Natural type for lambda expressions
using System.Reflection;

// var parse = s => int.Parse(s); // ERROR: CS8917 Not enough type info in the lambda
//Func<string, int> parse = s => int.Parse(s);
//var parse = (Func<string, int>)((string s) => int.Parse(s));
var parse = (string s) => int.Parse(s);

// Declared return type
// var choose = (bool b) => b ? 1 : "two"; // ERROR: CS8773 Can't infer return type
var choose = object (bool b) => b ? 1 : "two"; // Func<bool, object>
choose(true);

// Attributes
var choose2 = [Example(2)] object (bool b) => b ? 1 : "two";
var typeInfo = choose2.GetType().GetTypeInfo();
Console.WriteLine("The assembly qualified name of type is " + typeInfo.AssemblyQualifiedName);
var attrs = typeInfo.GetCustomAttributes();
foreach (var attr in attrs)
    Console.WriteLine("Attribute on type: " + attr.GetType().Name);
var methodInfo = choose2.GetMethodInfo();
attrs = methodInfo.GetCustomAttributes();
foreach (var attr in attrs)
    Console.WriteLine("Attribute on method: " + attr.GetType().Name);

public class ExampleAttribute : Attribute
{
    public ExampleAttribute(int n) { }
}
