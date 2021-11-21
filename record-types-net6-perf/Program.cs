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
using System.Data.SqlTypes;
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

    private DailyTemperatureRS rs1 = new(57, 30);
    private DailyTemperatureRS rs2 = new(57, 30);
    private DailyTemperatureS s1 = new(57, 30);
    private DailyTemperatureS s2 = new(57, 30);

    [Benchmark]
    public bool CompareRecordStructs()
        => rs1.Equals(rs2);

    [Benchmark]
    public bool CompareStructs()
        => s1.Equals(s2);

    [Benchmark]
    public DailyTemperatureRS CloneRecordStructs()
        => rs1 with { LowTemp = 20 };

    [Benchmark]
    public DailyTemperatureS CloneStructs()
        => new DailyTemperatureS() { HighTemp = s1.HighTemp, LowTemp = 20 };
}

public readonly record struct DailyTemperatureRS(double HighTemp, double LowTemp);

public struct DailyTemperatureS
{
    public double HighTemp { get; init; }
    public double LowTemp { get; init; }

    public DailyTemperatureS(int highTemp, int lowTemp)
        => (HighTemp, LowTemp) = (highTemp, lowTemp);
}