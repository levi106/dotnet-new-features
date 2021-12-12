using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private object _o = "hello";

    [Benchmark(Baseline = true)]
    public bool NonSealed() => _o is NonSealedType;

    [Benchmark]
    public bool Sealed() => _o is SealedType;
}

public class NonSealedType { }
public sealed class SealedType { }