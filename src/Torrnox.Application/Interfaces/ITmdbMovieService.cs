using LightResults;
using Torrnox.Application.Dto;
using Torrnox.Core.Tmdb;

namespace Torrnox.Application.Interfaces;

public interface ITmdbMovieService
{
    Task<Result<TmdbResponse<TmdbMovie>>> GetPopularMoviesAsync(int page, CancellationToken cancellationToken = default);
}
