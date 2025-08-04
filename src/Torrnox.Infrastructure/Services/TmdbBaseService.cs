using System;
using System.Net.Http.Json;
using LightResults;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Torrnox.Application.Configurations;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Entities;
using Torrnox.Infrastructure.Extensions;

namespace Torrnox.Infrastructure.Services;

public abstract class TmdbBaseService
{
    private readonly HttpClient _httpClient;
    private readonly ICacheService _cacheService;
    private RestClient _restClient;

    public TmdbBaseService(HttpClient httpClient, ICacheService cacheService)
    {
        ConfigService.OnConfigChanged += (_, _) => InitializeClient();
        _httpClient = httpClient;
        _cacheService = cacheService;
        InitializeClient();
    }

    protected Task<Result<T>> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        => SendAsync<T>(new RestRequest(endpoint, Method.Get), cancellationToken);

    protected Task<Result<T>> GetAsync<T>(string endpoint, IDictionary<string,string> parameters, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(endpoint, Method.Get);
        foreach (var param in parameters.OrderBy(p => p.Key))
        {
            request.AddParameter(param.Key, param.Value);
        }
        return SendAsync<T>(request, cancellationToken);
    }

    protected Task<Result<TResult>> PostAsync<TResult, TBody>(string endpoint, TBody body, CancellationToken cancellationToken = default) where TBody : notnull
        => SendAsync<TResult>(new RestRequest(endpoint, Method.Post).AddBody(body), cancellationToken);

    protected async Task<Result<T>> SendAsync<T>(RestRequest request, CancellationToken cancellationToken = default)
    {
        var data = await _cacheService.GetOrAdd(_restClient.GetRequestHash(request), async () =>
        {
            var response = await _restClient.ExecuteAsync<T>(request, cancellationToken: cancellationToken);

            if (response is null || !response.IsSuccessful || response.Data is null)
                return Result.Failure<T>();

            return response.Data;
        });

        return data;
    }

    private void InitializeClient()
    {
        _restClient = new RestClient(_httpClient, new RestClientOptions
        {
            BaseUrl = new Uri("https://api.themoviedb.org/3/")
        });

        _restClient
            .AddDefaultHeaders(new Dictionary<string, string>
            {
                ["Accept"] = "application/json",
                ["Authorization"] = $"Bearer {ConfigService.Config.TmdbApiKey}"
            })
            .AddDefaultQueryParameter("language", ConfigService.Config.Language);
    }
}
