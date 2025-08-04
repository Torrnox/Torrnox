using System;
using LightResults;
using Torrnox.Application.Dto;
using Torrnox.Application.Interfaces;

namespace Torrnox.Application.Services;

public sealed class MovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ITmdbMovieService _tmdbMovieService;

    public MovieService(IMovieRepository movieRepository, ITmdbMovieService tmdbMovieService)
    {
        _movieRepository = movieRepository;
        _tmdbMovieService = tmdbMovieService;
    }

    // public async Task<Result<MovieDto[]>> GetPopularMoviesAsync(CancellationToken cancellationToken = default)
    // {
    //     var moviesResult = await _tmdbMovieService.GetPopularMoviesAsync(cancellationToken);
    //     if (moviesResult.IsFailure(out var error, out var movies))
    //         return Result.Failure<MovieDto[]>(error);

        
    // }
}
