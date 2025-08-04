using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Torrnox.Application.Configurations;
using Torrnox.Core;
using Torrnox.Core.Enums;
using Torrnox.Infrastructure.Services;
using YamlDotNet.Serialization;

namespace Torrnox.Infrastructure.Data;

public sealed class CacheDataContextFactory : IDesignTimeDbContextFactory<CacheDataContext>
{
    public CacheDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CacheDataContext>();

        optionsBuilder.UseSqlite(
            $"Data Source={Path.Combine(AppConstants.BasePath, "cache.dat")}",
            o => o.MigrationsAssembly("Torrnox.Migrations.Sqlite"));

        return new CacheDataContext(optionsBuilder.Options);
    }
}
