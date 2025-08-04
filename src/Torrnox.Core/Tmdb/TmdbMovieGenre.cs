using System;
using System.Text.Json.Serialization;

namespace Torrnox.Application.Dto;

public sealed class TmdbMovieGenre
{
    [property: JsonPropertyName("id")]
    public required int Id { get; set; }

    [property: JsonPropertyName("name")]
    public required string Name { get; set; }
}
