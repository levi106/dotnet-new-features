// https://gist.github.com/layomia/2c236f74b250ed06e0cac5435f78e2cc
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
    private MemoryStream _memoryStream;
    private Utf8JsonWriter _jsonWriter;
    private Person _person;

    [GlobalSetup]
    public void Setup()
    {
        _memoryStream = new MemoryStream();
        _jsonWriter = new Utf8JsonWriter(_memoryStream);
        _person = DataGenerator.GetPerson();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _memoryStream.Dispose();
        _jsonWriter.Dispose();
    }

    [Benchmark(Baseline = true)]
    public void Serializer()
    {
        JsonSerializer.Serialize(_jsonWriter, _person);
        _memoryStream.SetLength(0);
        _jsonWriter.Reset();
    }

    [Benchmark]
    public void SrcGenSerializer()
    {
        JsonSerializer.Serialize(_jsonWriter, _person, MyJsonContext.Default.Person);
        _memoryStream.SetLength(0);
        _jsonWriter.Reset();
    }
}

[JsonSerializable(typeof(Person[]))]
internal partial class MyJsonContext : JsonSerializerContext { }

internal static class DataGenerator
{
    private static readonly string[] FirstNames = new[]
    {
        "Steffan", "Garin", "Fahad", "Eliana", "Thea", "Edmund", "Layla", "Tony", "Zakir", "Ariyah"
    };

    private static readonly string[] LastNames = new[]
    {
        "English", "Holder", "Beech", "Simon", "Briggs", "Terry", "Horton", "Leblanc", "Rodriguez", "Atkins"
    };

    public static Person GetPerson() => GetPeople(1)[0];

    public static Person[] GetPeople(int num)
    {
        Random rng = new();

        return Enumerable.Range(1, num).Select(indexer => new Person()
        {
            FirstName = FirstNames[rng.Next(FirstNames.Length)],
            LastName = LastNames[rng.Next(LastNames.Length)]
        }).ToArray();
    }
}

internal sealed class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}