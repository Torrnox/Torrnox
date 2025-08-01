using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Entities;

namespace Torrnox.Infrastructure.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Movie>()
            .ToTable("movies")
            .HasKey(x => x.Id);
    }
}