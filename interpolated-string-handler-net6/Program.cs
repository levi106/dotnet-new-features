using System.Runtime.CompilerServices;
using System.Text;

#region interpolated string

string name = "Frodo";
int age = 51;
string message = $"{name}: {age}";
Console.WriteLine(message);

#endregion

#region default interpolated string handler

DefaultInterpolatedStringHandler handler = new(2, 2);
handler.AppendFormatted(name);
handler.AppendLiteral(": ");
handler.AppendFormatted(age);
string message2 = handler.ToString();
Console.WriteLine(message2);

#endregion

#region interpolated string handler

var logger = new Logger() { EnabledLevel = LogLevel.Warning };
var time = DateTime.Now;

logger.LogMessage(LogLevel.Error, $"Error Level. CurrentTime: {time}. This is an error. It will be printed.");
logger.LogMessage(LogLevel.Trace, $"Trace Level. CurrentTime: {time}. This won't be printed.");
logger.LogMessage(LogLevel.Warning, $"Warning Level. This warning is a string, not an interpolated string expression.");

logger.LogMessage(LogLevel.Error, $"Error Level. CurrentTime: {time}. The time doesn't use formatting.");
logger.LogMessage(LogLevel.Error, $"Error Level. CurrentTime: {time:t}. This is an error. It will be printed.");
logger.LogMessage(LogLevel.Trace, $"Trace Level. CurrentTime: {time:t}. This won't be printed.");

public enum LogLevel
{
    Off,
    Critical,
    Error,
    Warning,
    Information,
    Trace
}

public class Logger
{
    public LogLevel EnabledLevel { get; init; } = LogLevel.Error;

    public void LogMessage(LogLevel level, string msg)
    {
        if (EnabledLevel < level) return;
        Console.WriteLine(msg);
    }

    public void LogMessage(LogLevel level, [InterpolatedStringHandlerArgument("", "level")]LogInterpolatedStringHandler builder)
    {
        if (EnabledLevel < level) return;
        Console.WriteLine(builder.GetFormattedText());
    }
}

[InterpolatedStringHandler]
public ref struct LogInterpolatedStringHandler
{
    StringBuilder builder;
    private readonly bool enabled = true;

    public LogInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        builder = new StringBuilder(literalLength);
        Console.WriteLine($"\tliteral length: {literalLength}, formattedCount: {formattedCount}");
    }

    public LogInterpolatedStringHandler(int literalLength, int formattedCount, Logger logger, LogLevel logLevel, out bool isEnabled)
    {
        isEnabled = enabled = logger.EnabledLevel >= logLevel;
        Console.WriteLine($"\tliteral length: {literalLength}, formattedCount: {formattedCount}, logLevel: {logLevel}");
        builder = new StringBuilder(literalLength);
    }

    public void AppendLiteral(string s)
    {
        Console.WriteLine($"\tAppendLiteral called: {{{s}}}");
        if (!enabled) return;

        builder.Append(s);
        Console.WriteLine($"\tAppended the literal string");
    }

    public void AppendFormatted<T>(T t)
    {
        Console.WriteLine($"\tAppendFormatted called: {{{t}}} is of type {typeof(T)}");
        if (!enabled) return;

        builder.Append(t?.ToString());
        Console.WriteLine($"\tAppended the formatted object");
    }

    public void AppendFormatted<T>(T t, string format) where T : IFormattable
    {
        Console.WriteLine($"\tAppendFormatted (IFormattable version) called: {t} with format {{{format}}} is of type {typeof(T)},");
        if (!enabled) return;

        builder.Append(t?.ToString(format, null));
        Console.WriteLine($"\tAppended the formatted object");
    }

    internal string GetFormattedText() => builder.ToString();
}

#endregion