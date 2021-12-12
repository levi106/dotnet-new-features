using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private SealedType _sealedInstance = new();
    private SealedType[] _sealedArray = new SealedType[1_000_000];
    private NonSealedType _nonSealedInstance = new();
    private NonSealedType[] _nonSealedArray = new NonSealedType[1_000_000];

    [Benchmark(Baseline = true)]
    public void NonSealed()
    {
        NonSealedType inst = _nonSealedInstance;
        NonSealedType[] arr = _nonSealedArray;
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = inst;
        }
    }

    [Benchmark]
    public void Sealed()
    {
        SealedType inst = _sealedInstance;
        SealedType[] arr = _sealedArray;
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = inst;
        }
    }
}

public class NonSealedType { }
public sealed class SealedType { }