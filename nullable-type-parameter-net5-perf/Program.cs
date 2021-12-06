#nullable enable

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

namespace nullable_type_parameter_net5_perf
{
    public class Program
    {
        public static void Main(string[] args) =>
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                             .Run(args, DefaultConfig.Instance
                //.WithSummaryStyle(new SummaryStyle(CultureInfo.InvariantCulture, printUnitsInHeader: false, SizeUnit.B, TimeUnit.Microsecond))
                );

        private A a = new A();

        [Benchmark]
        public int? TryF1()
            => a.TryF1(1, out int? t) ? t : null;

        [Benchmark]
        public int? F1()
            => a.F1<object?, int?>(1);
    }

    public class A
    {
        public bool TryF1<T>(T? s, out T? t)
        {
            if (s is null)
            {
                t = default;
                return false;
            }
            else
            {
                t = s;
                return true;
            }
        }

        public TResult? F1<TSource, TResult>(TSource value)
        {
            return (TResult?)(dynamic?)value ?? default;
        }
    }
}
