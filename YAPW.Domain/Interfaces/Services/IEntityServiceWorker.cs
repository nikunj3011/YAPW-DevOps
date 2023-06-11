using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces.Services;

public interface IEntityServiceWorker<TEntity, Tcontext> where TEntity : EntityBase where Tcontext : DataContext
{
    Task SaveBulkAsync<TEntity>(
       List<TEntity> entities,
       ServiceWorkerActionType serviceWorkerActionType,
       List<string> propertiesToExclude,
       bool withChildren,
       bool preserveInsertOrder,
       List<string> updateProperties,
       bool trackChanges) where TEntity : EntityBase;

    Task SaveAsync();

    void Save();
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollBackTransaction();
}

public enum ServiceWorkerActionType
{
    Add,
    Update,
    Delete,
    AddOrUpdate,
}