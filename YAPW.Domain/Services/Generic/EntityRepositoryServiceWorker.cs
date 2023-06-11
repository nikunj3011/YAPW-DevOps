using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Repositories.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Services.Generic
{
    public class EntityRepositoryServiceWorker<TEntityRepository, TEntity, TContext>
        where TEntityRepository : EntityRepository<TEntity, TContext>
        where TEntity : EntityBase
        where TContext : DataContext
    {
        private readonly TContext _context;

        public TEntityRepository Repository { get; }

        public EntityRepositoryServiceWorker(TContext context, TEntityRepository repository)
        {
            _context = context;
            Repository = repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveBulkAsync(
            List<TEntity> entities,
            ServiceWorkerActionType serviceWorkerActionType = ServiceWorkerActionType.Add,
            List<string> propertiesToExlude = null,
            bool withChildren = false,
            bool preserveInsertOrder = false,
            List<string> updateProperties = null,
            bool trackChanges = false)
        {
            var bulkConfig = new BulkConfig
            {
                SqlBulkCopyOptions = SqlBulkCopyOptions.FireTriggers,
                PropertiesToExclude = propertiesToExlude,
                SetOutputIdentity = withChildren,
                PreserveInsertOrder = preserveInsertOrder,
                UpdateByProperties = updateProperties,
                TrackingEntities = trackChanges
            };

            switch (serviceWorkerActionType)
            {
                case ServiceWorkerActionType.Add:
                    await _context.BulkInsertAsync(entities, bulkConfig);
                    break;

                case ServiceWorkerActionType.Update:
                    await _context.BulkUpdateAsync(entities, bulkConfig);
                    break;

                case ServiceWorkerActionType.Delete:
                    throw new NotImplementedException();
                case ServiceWorkerActionType.AddOrUpdate:
                    await _context.BulkInsertOrUpdateAsync(entities, bulkConfig);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}