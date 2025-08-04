using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Entities;

namespace Torrnox.Infrastructure.Data;

public sealed class EntityDataContext : DbContext
{
    public EntityDataContext(DbContextOptions<EntityDataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MovieEntity>(entity =>
        {
            entity.ToTable("movies");
            entity.HasMany(x => x.Genres);
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.ExternalId, x.Language }).IsUnique();
        });

        builder.Entity<MovieGenreEntity>(entity =>
        {
            entity.ToTable("movies_genres");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.ExternalId, x.Language }).IsUnique();
        });
    }
}