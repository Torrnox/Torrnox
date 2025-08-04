using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Entities;

namespace Torrnox.Web.Controllers;

[Route(WebConstants.ApiPrefix + "/movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly ITmdbMovieService _tmdbService;

    public MovieController(ITmdbMovieService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    [HttpGet("popular")]
    public async Task<ActionResult<MovieEntity[]>> GetPopularMoviesAsync(int page = 1, CancellationToken cancellationToken = default)
    {
        var result = await _tmdbService.GetPopularMoviesAsync(page, cancellationToken);
        if (result.IsFailure(out var error, out var movies))
        {
            return Problem(detail: error.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok(movies);
    }
}
