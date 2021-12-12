using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Buffers.Binary;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    private byte[] _buffer = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

    [Benchmark(Baseline = true)]
    public long Old() => Old(_buffer, 0);

    [Benchmark]
    public long New() => New(_buffer, 0);

    private static unsafe long Old(byte[] value, int startIndex)
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/fixed-statement
        // The fixed statement prevents the garbage collector from relocating a movable variable.
        fixed (byte* pbyte = &value[startIndex])
        {
            int i1 = (*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3));
            int i2 = (*(pbyte + 4) << 24) | (*(pbyte + 5) << 16) | (*(pbyte + 6) << 8) | (*(pbyte + 7));
            return (uint)i2 | ((long)i1 << 32);
        }
    }

    private static long New(byte[] value, int startIndex) =>
        BinaryPrimitives.ReadInt64BigEndian(value.AsSpan(startIndex));
        // https://github.com/dotnet/runtime/blob/57bfe474518ab5b7cfe6bf7424a79ce3af9d6657/src/libraries/System.Private.CoreLib/src/System/Buffers/Binary/ReaderBigEndian.cs
}
