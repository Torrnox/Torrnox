using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Torrnox.Application.Interfaces;
using Torrnox.Infrastructure.Data;
using Torrnox.Infrastructure.Extensions;
using Torrnox.Infrastructure.Services;
using Torrnox.Web;
using Torrnox.Web.Extensions;
using Torrnox.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var loggingService = new LoggingService();

builder.Services.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EntityDataContext>();
    await db.Database.MigrateAsync();
}

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api", options =>
    {
        options.BaseServerUrl = WebConstants.ApiPrefix;
    });
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var entityContext = scope.ServiceProvider.GetRequiredService<EntityDataContext>();
    await entityContext.ApplyMigrationsAsync();

    var cacheContext = scope.ServiceProvider.GetRequiredService<CacheDataContext>();
    await cacheContext.ApplyMigrationsAsync();
}

app.Run();
