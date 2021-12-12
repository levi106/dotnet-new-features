using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Globalization;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private string intString = "123456";

    [Benchmark(Baseline = true)]
    public int nullCulture() => int.Parse(intString, NumberStyles.Integer);

    [Benchmark]
    public int invaliantCulture() => int.Parse(intString, NumberStyles.Integer, provider: CultureInfo.InvariantCulture);
}
