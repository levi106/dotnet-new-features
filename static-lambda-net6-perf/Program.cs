using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    [Benchmark(Baseline = true)]
    public int Lambda() => M("\"Stephen\"");

    [Benchmark]
    public int StaticLambda() => N("\"Stephen\"");

    public static int M(string quotedName)
    {
        int i = 12345;
        var f = int (string x) => x.Length;
        return f(quotedName) + i;
    }

    public static int N(string quotedName)
    {
        int i = 12345;
        var f = static int (string x) => x.Length;
        return f(quotedName) + i;
    }
}
