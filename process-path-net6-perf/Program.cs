using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    [Benchmark(Baseline = true)]
    public string GetPathViaProcess() =>
        Process.GetCurrentProcess().MainModule.FileName;

    [Benchmark]
    public string GetPathViaEnvironment() =>
        Environment.ProcessPath;
}
