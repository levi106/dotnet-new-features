using System.Globalization;
using System.Runtime.CompilerServices;

int major = 6, minor = 0, build = 100, revision = 7;

string s1 = A.Create($"{major}.{minor}.{build}.{revision}");
Console.WriteLine(s1);

string s2 = A.Create(CultureInfo.InvariantCulture, $"{major}.{minor}.{build}.{revision}");
Console.WriteLine(s2);

internal static class A
{
    public static string Create(ref DefaultInterpolatedStringHandler handler) =>
        handler.ToStringAndClear();

    public static string Create(IFormatProvider? provider, [InterpolatedStringHandlerArgument("provider")] ref DefaultInterpolatedStringHandler handler) =>
        handler.ToStringAndClear();
}