using System;
using System.Text.Json.Serialization;

namespace Torrnox.Application.Dto;

public sealed class TmdbMovie
{
    [property: JsonPropertyName("id")]
    public int Id { get; set; }

    [property: JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [property: JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [property: JsonPropertyName("genre_ids")]
    public int[] GenreIds { get; set; }

    [property: JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; }

    [property: JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; }

    [property: JsonPropertyName("overview")]
    public string Overview { get; set; }

    [property: JsonPropertyName("popularity")]
    public double Popularity { get; set; }

    [property: JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [property: JsonPropertyName("release_date")]
    public DateOnly ReleaseDate { get; set; }

    [property: JsonPropertyName("title")]
    public string Title { get; set; }

    [property: JsonPropertyName("video")]
    public bool Video { get; set; }

    [property: JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [property: JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }
}
