using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    [Benchmark(Baseline = true)]
    public string Substring() => M("\"Stephen\"");

    [Benchmark]
    public string Span() => N("\"Stephen\"");

    public static string M(string quotedName) =>
        "Hello," + quotedName.Substring(1, quotedName.Length - 2);

    public static string N(string quotedName) =>
        string.Concat("Hello,", quotedName.AsSpan(1, quotedName.Length - 2));
}
