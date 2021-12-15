using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private string _str;

    [GlobalSetup]
    public async Task Setup()
    {
        using var hc = new HttpClient();
        _str = await hc.GetStringAsync("https://www.gutenberg.org/cache/epub/3200/pg3200.txt");
    }

    [Benchmark]
    public string Yell() => _str.Replace(".", "!");

    [Benchmark]
    public string ConcatLines() => _str.Replace("\n", "");

    [Benchmark]
    public string NormalizeEndings() => _str.Replace("\r\n", "\n");
}
