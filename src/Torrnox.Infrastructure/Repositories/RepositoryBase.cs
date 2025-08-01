using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Torrnox.Core.Entities;
using Torrnox.Core.Interfaces;

namespace Torrnox.Infrastructure.Repositories;

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
{
    protected DbContext DbContext { get; init; }

    protected RepositoryBase(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <inheritdoc/>
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().AddRange(entities);

        await SaveChangesAsync(cancellationToken);

        return entities;
    }

    /// <inheritdoc/>
    public virtual async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;
        
        DbContext.Set<T>().Update(entity);

        var result = await SaveChangesAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<int> UpdateRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        foreach (var entity in entities)
        {
            entity.UpdatedOn = now;
        }
        
        DbContext.Set<T>().UpdateRange(entities);

        var result = await SaveChangesAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Remove(entity);

        var result = await SaveChangesAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<int> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().RemoveRange(entities);

        var result = await SaveChangesAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await DbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AnyAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual IAsyncEnumerable<T> AsAsyncEnumerable()
    {
        return DbContext.Set<T>().AsAsyncEnumerable();
    }
}
