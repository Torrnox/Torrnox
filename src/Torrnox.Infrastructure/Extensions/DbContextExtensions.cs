using System;
using Microsoft.EntityFrameworkCore;

namespace Torrnox.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static async Task ApplyMigrationsAsync(this DbContext context)
    {
        var migrations = context.Database.GetMigrations().ToList();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        await context.Database.MigrateAsync();
    }
}
