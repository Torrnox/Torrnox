using System;

namespace Torrnox.Application.Dto;

public sealed class MovieDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public MovieGenreDto[] Genres { get; set; } = [];
}
