using System.Runtime.CompilerServices;

try
{
    int[] array = new int[10];
    Validate(array.Length == 1);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

void Validate(bool condition, [CallerArgumentExpression("condition")] string? message = null)
{
    if (!condition)
    {
        throw new InvalidOperationException($"Argument failed validation: <{message}>");
    }
}