using Microsoft.Extensions.Logging;
using System.Text.Json;

var logger = LoggerFactory.Create(
        builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConsole();
            builder.AddJsonConsole(
                options =>
                options.JsonWriterOptions = new JsonWriterOptions()
                {
                    Indented = true
                });

        })
    .CreateLogger<Program>();

string hostname = "https://www.bing.com";

logger.LogInformation($"hostname is {hostname}");
logger.LogInformation("hostname is {hostname}", hostname);


logger.LogHostname(hostname);
logger.LogHostname2(hostname);

internal static class Log
{
    private static readonly Action<ILogger, string, Exception?> _printHostname =
        LoggerMessage.Define<string>(LogLevel.Information, new EventId(1, nameof(LogHostname)), "hostname is {hostname}");

    public static void LogHostname(this ILogger logger, string hostname)
    {
        _printHostname(logger, hostname, null);
    }
}

internal static partial class LogSrcGen
{
    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "hostname is {hostname}")]
    public static partial void LogHostname2(this ILogger logger, string hostname);
}