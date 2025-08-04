using LightResults;
using Torrnox.Application.Dto;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Tmdb;

namespace Torrnox.Infrastructure.Services;

public sealed class TmdbMovieService : TmdbBaseService, ITmdbMovieService
{
    public TmdbMovieService(HttpClient httpClient, ICacheService cacheService)
        : base(httpClient, cacheService)
    {

    }

    public async Task<Result<TmdbResponse<TmdbMovie>>> GetPopularMoviesAsync(int page, CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<TmdbResponse<TmdbMovie>>("movie/popular", new Dictionary<string,string>
        {
            ["page"] = page.ToString()
        }, cancellationToken);

        if (response.IsFailure(out var error, out var results))
            return Result.Failure<TmdbResponse<TmdbMovie>>(error);

        return results;
    }

    public async Task<Result<TmdbResponse<TmdbMovieGenre>>> GetMoviesGenresAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<TmdbResponse<TmdbMovieGenre>>("genre/movie/list", cancellationToken);

        if (response.IsFailure(out var error, out var results))
            return Result.Failure<TmdbResponse<TmdbMovieGenre>>(error);

        return results;
    }
}
