using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private SealedType[] _sealedArray = new SealedType[10];
    private NonSealedType[] _nonSealedArray = new NonSealedType[10];

    [Benchmark(Baseline = true)]
    public Span<NonSealedType> NonSealed() => _nonSealedArray;

    [Benchmark]
    public Span<SealedType> Sealed() => _sealedArray;
}

public class NonSealedType { }
public sealed class SealedType { }