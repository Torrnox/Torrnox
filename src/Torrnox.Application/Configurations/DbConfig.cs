using Torrnox.Core.Enums;

namespace Torrnox.Application.Configurations;

public sealed class DbConfig : IEquatable<DbConfig>
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public bool Equals(DbConfig? other)
    {
        if (other == null)
            return false;

        return
            Host == other.Host &&
            Port == other.Port &&
            Database == other.Database &&
            Username == other.Username &&
            Password == other.Password;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as AppConfig);
    }

    public static bool operator ==(DbConfig? a, DbConfig? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(DbConfig? a, DbConfig? b) => !(a == b);

    public override int GetHashCode()
    {
        return HashCode.Combine(Host, Port, Database, Username, Password);
    }
}

