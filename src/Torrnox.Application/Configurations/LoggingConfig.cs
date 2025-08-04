using System;
using Microsoft.Extensions.Logging;

namespace Torrnox.Application.Configurations;

public sealed class LoggingConfig : IEquatable<LoggingConfig>
{
    public LogLevel AppLoggingLevel { get; set; } = LogLevel.Debug;
    public LogLevel MicrosoftLoggingLevel { get; set; } = LogLevel.Debug;
    public LogLevel SystemLoggingLevel { get; set; } = LogLevel.Debug;

    public bool Equals(LoggingConfig? other)
    {
        if (other == null)
            return false;

        return
            AppLoggingLevel == other.AppLoggingLevel &&
            MicrosoftLoggingLevel == other.MicrosoftLoggingLevel &&
            SystemLoggingLevel == other.SystemLoggingLevel;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as AppConfig);
    }

    public static bool operator ==(LoggingConfig? a, LoggingConfig? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(LoggingConfig? a, LoggingConfig? b) => !(a == b);

    public override int GetHashCode()
    {
        return HashCode.Combine(AppLoggingLevel, MicrosoftLoggingLevel, SystemLoggingLevel);
    }
}

