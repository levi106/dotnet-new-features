using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Order;
using Perfolizer.Horology;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
#if NETCOREAPP3_0_OR_GREATER
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public class Program
{
    public static void Main(string[] args) =>
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                         .Run(args, DefaultConfig.Instance
                                                 //.WithSummaryStyle(new SummaryStyle(CultureInfo.InvariantCulture, printUnitsInHeader: false, SizeUnit.B, TimeUnit.Microsecond))
            );

    private int Major = 6;
    private int Minor = 0;
    private int Build = 100;
    private int Revision = 21380;

    [Benchmark(Baseline = true)]
    public string StringFormat()
    {
        object[] array = new object[4];
        array[0] = Major;
        array[1] = Minor;
        array[2] = Build;
        array[3] = Revision;
        return String.Format("{0}.{1}.{2}.{3}", array);
    }

    [Benchmark]
    public string InterpolatedString()
        => $"{Major}.{Minor}.{Build}.{Revision}";

    [Benchmark]
    public string InitialBufferSpace()
    => String.Create(null, stackalloc char[64], $"{Major}.{Minor}.{Build}.{Revision}");

    [Benchmark]
    public string InitialBufferSpaceWithFormatProvider()
    => String.Create(CultureInfo.InvariantCulture, stackalloc char[64], $"{Major}.{Minor}.{Build}.{Revision}");

    [Benchmark]
    public string InterpolatedStringHandler()
    {
        var h = new DefaultInterpolatedStringHandler(3, 4);
        h.AppendFormatted(Major);
        h.AppendLiteral(".");
        h.AppendFormatted(Minor);
        h.AppendLiteral(".");
        h.AppendFormatted(Build);
        h.AppendLiteral(".");
        h.AppendFormatted(Revision);
        return h.ToStringAndClear();
    }
}