using System;
using System.Text.Json;
using LightResults;
using Microsoft.EntityFrameworkCore;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Entities;
using Torrnox.Infrastructure.Data;

namespace Torrnox.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private readonly CacheDataContext _context;

    public CacheService(CacheDataContext context)
    {
        _context = context;
    }

    public Task<Result<T>> GetOrAdd<T>(string key, Func<Task<Result<T>>> factory)
        => GetOrAdd(key, factory, DateTimeOffset.UtcNow + TimeSpan.FromHours(24));

    public async Task<Result<T>> GetOrAdd<T>(string key, Func<Task<Result<T>>> factory, DateTimeOffset expirationTime)
    {
        var cachedItem = await _context.Set<CacheEntity>().AsNoTracking().FirstOrDefaultAsync(c => c.Key == key);
        if (cachedItem != null)
        {
            var cachedValue = JsonSerializer.Deserialize<T>(cachedItem.Value);
            return cachedValue!;
        }

        var result = await factory();

        if (result.IsFailure(out var error, out var value))
            return Result.Failure<T>(error);

        var serializedValue = JsonSerializer.Serialize(value);
        _context.Set<CacheEntity>().Add(new CacheEntity { Key = key, Value = serializedValue, ExpireAt = expirationTime });
        await _context.SaveChangesAsync();

        return value;
    }
}
