using System;
using LightResults;

namespace Torrnox.Application.Interfaces;

public interface ICacheService
{
    Task<Result<T>> GetOrAdd<T>(string key, Func<Task<Result<T>>> factory);
    Task<Result<T>> GetOrAdd<T>(string key, Func<Task<Result<T>>> factory, DateTimeOffset expirationTime);
}
