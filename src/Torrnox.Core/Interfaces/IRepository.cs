namespace Torrnox.Core.Interfaces;

public interface IRepository<T> where T : class
{
    /// <inheritdoc/>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<ICollection<T>> AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> UpdateRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    /// <inheritdoc/>
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    IAsyncEnumerable<T> AsAsyncEnumerable();
}