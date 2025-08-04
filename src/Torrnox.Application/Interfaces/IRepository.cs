using System;
using LightResults;
using Torrnox.Core.Entities;

namespace Torrnox.Application.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<Result<T>> UpsertAsync(T entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<T>> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default);
}
