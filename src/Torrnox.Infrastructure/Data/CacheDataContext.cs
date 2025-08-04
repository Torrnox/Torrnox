using System;
using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Entities;

namespace Torrnox.Infrastructure.Data;

public sealed class CacheDataContext : DbContext
{
    public CacheDataContext(DbContextOptions<CacheDataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CacheEntity>(entity =>
        {
            entity.ToTable("cache");
            entity.HasKey(x => x.Key);
            entity.HasIndex(x => x.Key).IsUnique();
        });
    }
}
