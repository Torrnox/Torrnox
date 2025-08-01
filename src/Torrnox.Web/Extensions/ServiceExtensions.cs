using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Interfaces;
using Torrnox.Infrastructure.Data;
using Torrnox.Infrastructure.Repositories;

namespace Torrnox.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddDatabase();
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(builder =>
        {
            builder.UseSqlite("Data Source=torrn.dat");
        });
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        
        return services;
    }
}