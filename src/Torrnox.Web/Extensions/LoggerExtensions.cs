using System;
using Serilog.Events;

namespace Torrnox.Web.Extensions;

public static class LoggerExtensions
{
    public static LogEventLevel ToSerilog(this LogLevel level)
    => level switch
    {
        LogLevel.Trace => LogEventLevel.Verbose,
        LogLevel.Debug => LogEventLevel.Debug,
        LogLevel.Information => LogEventLevel.Information,
        LogLevel.Warning => LogEventLevel.Warning,
        LogLevel.Error => LogEventLevel.Error,
        LogLevel.Critical => LogEventLevel.Fatal,
        _ => throw new ArgumentException("Invalid log level", nameof(level)),
    };
}
