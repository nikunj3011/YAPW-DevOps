using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Repositories.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Services.Generic
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// 

    public class EntityServiceWorker<TEntity, TContext> : IEntityServiceWorker<TEntity, TContext> where TEntity : EntityBase where TContext : DataContext
    {
        private readonly TContext _context;
        private EntityRepository<TEntity, TContext> entityRepository;
        //private OrderRepository<Order, TContext> orderRepository;
        //private OrderStatusRepository<OrderStatus, TContext> orderStatusRepository;


        public EntityServiceWorker(TContext context) => _context = context;

        public EntityRepository<TEntity, TContext> EntityRepository => entityRepository ??= new EntityRepository<TEntity, TContext>(_context);
        //public OrderRepository<Order, TContext> OrderRepository => orderRepository ??= new OrderRepository<Order, TContext>(_context);
        //public OperationQuestionRepository<OperationQuestion, TContext> OperationQuestionRepository => operationQuestionRepository ??= new OperationQuestionRepository<OperationQuestion, TContext>(_context);
        
        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync().ConfigureAwait(false);
        }

        public async Task RollBackTransaction()
        {
            await _context.Database.RollbackTransactionAsync().ConfigureAwait(false);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SaveBulkAsync<TEntity>(
            List<TEntity> entities,
            ServiceWorkerActionType serviceWorkerActionType = ServiceWorkerActionType.Add,
            List<string> propertiesToExlude = null,
            bool withChildren = false,
            bool preserveInsertOrder = false,
            List<string> updateProperties = null,
            bool trackChanges = false)
            where TEntity : EntityBase
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
                    await _context.BulkInsertAsync(entities, bulkConfig).ConfigureAwait(false);
                    break;

                case ServiceWorkerActionType.Update:
                    await _context.BulkUpdateAsync(entities, bulkConfig).ConfigureAwait(false);
                    break;

                case ServiceWorkerActionType.Delete:
                    throw new NotImplementedException();
                case ServiceWorkerActionType.AddOrUpdate:
                    await _context.BulkInsertOrUpdateAsync(entities, bulkConfig).ConfigureAwait(false);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
