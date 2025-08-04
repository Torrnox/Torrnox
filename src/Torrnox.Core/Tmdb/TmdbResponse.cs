using System;
using System.Text.Json.Serialization;

namespace Torrnox.Core.Tmdb;

public sealed class TmdbResponse<T>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public T[] Results { get; set; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}
