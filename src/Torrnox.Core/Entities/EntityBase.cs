

namespace Torrnox.Core.Entities;

public abstract class EntityBase
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public int ExternalId { get; init; }

    public string Language { get; set; } = "en-US";

    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedOn { get; set; } = null;
}