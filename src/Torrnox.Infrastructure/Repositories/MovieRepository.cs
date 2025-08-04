using System;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Entities;
using Torrnox.Infrastructure.Data;

namespace Torrnox.Infrastructure.Repositories;

public sealed class MovieRepository : Repository<MovieEntity>, IMovieRepository
{
    public MovieRepository(EntityDataContext dataContext) : base(dataContext)
    {
    }
}
