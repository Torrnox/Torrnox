using System;

namespace Torrnox.Core.Entities;

public sealed class CacheEntity
{
    public required string Key { get; set; }
    public required string Value { get; set; }
    public DateTimeOffset? ExpireAt { get; set; }
}
