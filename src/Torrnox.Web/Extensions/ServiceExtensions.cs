using Microsoft.EntityFrameworkCore;
using Torrnox.Application.Interfaces;
using Torrnox.Core;
using Torrnox.Core.Enums;
using Torrnox.Infrastructure.Data;
using Torrnox.Infrastructure.Repositories;
using Torrnox.Infrastructure.Services;

namespace Torrnox.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services
            .AddDatabase()
            .AddRepositories()
            .AddHttpClients();
            
        services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<ITmdbMovieService, TmdbMovieService>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<EntityDataContext>(builder =>
        {
            switch (ConfigService.Config.DbProvider)
            {
                case DbProvider.Sqlite:
                    builder.UseSqlite($"Data Source={Path.Combine(AppConstants.BasePath, "db.dat")}", o => o.MigrationsAssembly("Torrnox.Migrations.Sqlite"));
                    break;
                case DbProvider.PostgreSql:
                    if (ConfigService.Config.DbConfig is null)
                        throw new ArgumentException("DbConfig required for NPGSQL provider");

                    builder.UseNpgsql($"Host={ConfigService.Config.DbConfig.Host};Port={ConfigService.Config.DbConfig.Port};Database={ConfigService.Config.DbConfig.Database};UserID={ConfigService.Config.DbConfig.Username};Password={ConfigService.Config.DbConfig.Password}",
                    o => o.MigrationsAssembly("Torrnox.Migrations.Npgsql"));
                    break;
                default:
                    throw new ArgumentException("Invalid DbProvider");
            }
        });

        services.AddDbContext<CacheDataContext>(builder =>
        {
            builder.UseSqlite($"Data Source={Path.Combine(AppConstants.BasePath, "cache.dat")}", o => o.MigrationsAssembly("Torrnox.Migrations.Sqlite"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services
            .AddHttpClient<ITmdbMovieService>()
            .AddStandardResilienceHandler();

        services.AddScoped<ITmdbMovieService, TmdbMovieService>();

        return services;
    }
}
