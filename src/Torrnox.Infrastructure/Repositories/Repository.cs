using System;
using LightResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Torrnox.Application.Interfaces;
using Torrnox.Core.Entities;
using Torrnox.Infrastructure.Data;
using Torrnox.Infrastructure.Services;

namespace Torrnox.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected EntityDataContext Context { get; }

    protected string Language;


    public Repository(EntityDataContext dataContext)
    {
        Context = dataContext;
        Language = ConfigService.Config.Language;

        ConfigService.OnConfigChanged += (_, args) => Language = args.Config.Language;
    }

    public async Task<Result<T>> UpsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            var dbEntity = await Context.Set<T>().FindAsync([entity.Id], cancellationToken: cancellationToken);
            if (dbEntity is null)
            {
                entity.Language = Language;
                Context.Add(entity);
                dbEntity = entity;
            }
            else
            {
                entity.Adapt(dbEntity);
                dbEntity.UpdatedOn = DateTimeOffset.UtcNow;
            }

            await Context.SaveChangesAsync(cancellationToken);
            return dbEntity;
        }

        Context.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Context.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        if (result == 0)
            return Result.Failure();
        return Result.Success();
    }

    public async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (result is null)
           return Result.Failure<T>();
        return Result.Success(result);
    }

    public async Task<Result<T>> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var result = await Context.Set<T>().Where(x => x.ExternalId == externalId && x.Language == Language).FirstOrDefaultAsync(cancellationToken);
        if (result is null)
           return Result.Failure<T>();
        return Result.Success(result);
    }
}