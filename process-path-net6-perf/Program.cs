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

var data = await File.ReadAllBytesAsync(@"c:large_file.json");

// Safe but slow.
var copied = ByteString.CopyFrom(data);

// Unsafe but fast. Useful if you know data won't change.
var wrapped = UnsafeByteOperations.UnsafeWrap(data);