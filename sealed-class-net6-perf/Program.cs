using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private SealedType _sealed = new();
    private NonSealedType _nonSealed = new();

    [Benchmark(Baseline = true)]
    public int NonSealed() => _nonSealed.M() + 42;

    [Benchmark]
    public int Sealed() => _sealed.M() + 42;
}

public class BaseType
{
    public virtual int M() => 1;
}

public class NonSealedType : BaseType
{
    public override int M() => 2;
}

public sealed class SealedType : BaseType
{
    public override int M() => 2;
}