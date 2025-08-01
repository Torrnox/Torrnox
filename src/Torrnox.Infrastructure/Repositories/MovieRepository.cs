using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Entities;
using Torrnox.Core.Interfaces;

namespace Torrnox.Infrastructure.Repositories;

public sealed class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(DbContext dbContext) : base(dbContext)
    {
    }
}