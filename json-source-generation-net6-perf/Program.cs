using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Text.Json;
using System.Text.Json.Serialization;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private Message obj = new() { Data = "Hello World" };

    [Benchmark(Baseline = true)]
    public string Serializer() => JsonSerializer.Serialize(obj);

    [Benchmark]
    public string SrcGenSerializer() => JsonSerializer.Serialize(obj, MyJsonContext.Default.Message);
}

public struct Message
{
    public string Data { get; set; }
}

[JsonSerializable(typeof(Message))]
internal partial class MyJsonContext : JsonSerializerContext { }