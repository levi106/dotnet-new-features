using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

[DisassemblyDiagnoser(maxDepth: 1)]
[MemoryDiagnoser(displayGenColumns: false)]
public partial class Program
{
    ILogger<Program> _logger;
    private int Major = 6;
    private int Minor = 0;
    private int Build = 100;
    private int Revision = 21380;

    [GlobalSetup]
    public void Setup()
    {
        _logger = LoggerFactory.Create(
                builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information);
                    //builder.AddConsole();
                    //builder.AddJsonConsole(
                    //    options =>
                    //    options.JsonWriterOptions = new JsonWriterOptions()
                    //    {
                    //        Indented = true
                    //    });

                })
            .CreateLogger<Program>();
    }

    [Benchmark(Baseline = true)]
    public void LoggerExtensionMethod() =>
         _logger.LogInformation("{Major}.{Minor}.{Build}.{Revision}", Major, Minor, Build, Revision);

    [Benchmark]
    public void LoggerExtensionMethodDebugLevel() =>
        _logger.LogDebug("{Major}.{Minor}.{Build}.{Revision}", Major, Minor, Build, Revision);

    [Benchmark]
    public void HighPerformanceLogging() =>
        _logger.WriteVersionHP(Major, Minor, Build, Revision);

    [Benchmark]
    public void HighPerformanceLoggingDebugLevel() =>
        _logger.WriteVersionHPD(Major, Minor, Build, Revision);

    [Benchmark]
    public void CompileTimeLoggingSourceGeneration() =>
        _logger.WriteVersion(Major, Minor, Build, Revision);

    [Benchmark]
    public void CompileTimeLoggingSourceGenerationDebugLevel() =>
        _logger.WriteVersionD(Major, Minor, Build, Revision);

}

internal static partial class Log
{
    private static readonly Action<ILogger, int, int, int, int, Exception> _versionHP =
        LoggerMessage.Define<int, int, int, int>(
            LogLevel.Information,
            new EventId(0, nameof(WriteVersionHP)),
            "{major}.{minor}.{build}.{revision}");

    public static void WriteVersionHP(this ILogger logger, int major, int minor, int build, int revision) =>
        _versionHP(logger, major, minor, build, revision, default!);

    private static readonly Action<ILogger, int, int, int, int, Exception> _versionHPD =
        LoggerMessage.Define<int, int, int, int>(
            LogLevel.Debug,
            new EventId(1, nameof(WriteVersionHP)),
            "{major}.{minor}.{build}.{revision}");

    public static void WriteVersionHPD(this ILogger logger, int major, int minor, int build, int revision) =>
        _versionHPD(logger, major, minor, build, revision, default!);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "{major}.{minor}.{build}.{revision}")]
    public static partial void WriteVersion(this ILogger logger, int major, int minor, int build, int revision);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Debug,
        Message = "{major}.{minor}.{build}.{revision}")]
    public static partial void WriteVersionD(this ILogger logger, int major, int minor, int build, int revision);

}