using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces.Services.Generic
{
    public interface IServiceWorker : IGenericServiceWorker
    {
        Task SaveBulkAsync<TEntity>(
            List<TEntity> entities,
            ServiceWorkerActionType serviceWorkerActionType,
            List<string> propertiesToExclude,
            bool withChildren,
            bool preserveInsertOrder,
            List<string> updateProperties,
            bool trackChanges) where TEntity : EntityBase;
    }
}