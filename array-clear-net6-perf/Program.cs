using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private int[] _array = new int[10];

    [Benchmark(Baseline = true)]
    public void Old() => Array.Clear(_array, 0, _array.Length);

    [Benchmark]
    public void New() => Array.Clear(_array);
}
