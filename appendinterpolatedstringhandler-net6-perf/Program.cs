using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Text;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private int Major = 6;
    private int Minor = 0;
    private int Build = 100;
    private int Revision = 21380;

    [Benchmark]
    public string Version()
    {
        StringBuilder builder = new();
        AppendVersion(builder, Major, Minor, Build, Revision);
        return builder.ToString();
    }

    public static void AppendVersion(StringBuilder builder, int major, int minor, int build, int revision) =>
        builder.Append($"{major}.{minor}.{build}.{revision}");

}
