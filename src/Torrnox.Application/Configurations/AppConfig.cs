using Torrnox.Core.Enums;

namespace Torrnox.Application.Configurations;

public sealed class AppConfig : IEquatable<AppConfig>
{
    public bool SetupCompleted { get; set; } = false;
    public string? BaseUrl { get; set; } = null;
    public string? TmdbApiKey { get; set; } = null;
    public string Language { get; set; } = "en-US";
    public DbProvider DbProvider { get; set; } = DbProvider.Sqlite;
    public DbConfig? DbConfig { get; set; } = null;
    public LoggingConfig Logging { get; set; } = new();

    public bool Equals(AppConfig? other)
    {
        if (other == null)
            return false;

        return
            SetupCompleted == other.SetupCompleted &&
            BaseUrl == other.BaseUrl &&
            TmdbApiKey == other.TmdbApiKey &&
            Language == other.Language &&
            DbProvider == other.DbProvider &&
            DbConfig == other.DbConfig &&
            Logging == other.Logging;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as AppConfig);
    }

    public static bool operator ==(AppConfig? a, AppConfig? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(AppConfig? a, AppConfig? b) => !(a == b);

    public override int GetHashCode()
    {
        return HashCode.Combine(SetupCompleted, BaseUrl, TmdbApiKey, Language, DbProvider, DbConfig, Logging);
    }
}

