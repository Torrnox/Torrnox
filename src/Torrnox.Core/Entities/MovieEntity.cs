using System.Text.Json.Serialization;

namespace Torrnox.Core.Entities;

public sealed class MovieEntity : EntityBase
{
    public bool Adult { get; set; }

    public string BackdropPath { get; set; }

    public string OriginalLanguage { get; set; }

    public string OriginalTitle { get; set; }

    public string Overview { get; set; }

    public double Popularity { get; set; }

    public string PosterPath { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public string Title { get; set; }

    public bool Video { get; set; }

    public double VoteAverage { get; set; }

    public int VoteCount { get; set; }

    public List<MovieGenreEntity> Genres { get; set; } = [];
}