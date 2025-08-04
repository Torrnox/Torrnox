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

public sealed class EntityDataContextFactory : IDesignTimeDbContextFactory<EntityDataContext>
{
    public EntityDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EntityDataContext>();

        switch (args[0].ToLowerInvariant())
        {
            case "sqlite":
                optionsBuilder.UseSqlite(
                    $"Data Source={Path.Combine(AppConstants.BasePath, "db.dat")}",
                    o => o.MigrationsAssembly("Torrnox.Migrations.Sqlite"));

                break;
            case "npgsql":
                if (ConfigService.Config.DbConfig is null)
                    throw new ArgumentException("DbConfig required for NPGSQL provider");

                optionsBuilder.UseNpgsql(
                    $"Host={ConfigService.Config.DbConfig?.Host};Port={ConfigService.Config.DbConfig?.Port};Database={ConfigService.Config.DbConfig?.Database};UserID={ConfigService.Config.DbConfig?.Username};Password={ConfigService.Config.DbConfig?.Password}",
                    o => o.MigrationsAssembly("Torrnox.Migrations.Npgsql"));
                break;
            default:
                throw new ArgumentException("Invalid DbProvider");
        }

        return new EntityDataContext(optionsBuilder.Options);
    }
}
