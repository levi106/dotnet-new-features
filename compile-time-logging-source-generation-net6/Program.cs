using Microsoft.Extensions.Logging;
using System.Text.Json;

ILogger<Program> logger = LoggerFactory.Create(
    builder =>
    builder.AddJsonConsole(
        options =>
        options.JsonWriterOptions = new JsonWriterOptions()
        {
            Indented = true
        }))
    .CreateLogger<Program>();

logger.CouldNotOpenSocket("www.bing.com");

internal static partial class Log
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Critical,
        Message = "Could not open socket to `{hostName}`")]
    public static partial void CouldNotOpenSocket(this ILogger logger, string hostName);
}