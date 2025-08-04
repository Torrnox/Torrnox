using System;

namespace Torrnox.Application.Dto;

public sealed class MovieGenreDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
