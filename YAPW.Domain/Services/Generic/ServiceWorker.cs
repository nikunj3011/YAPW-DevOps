using Azure;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Repositories.Generic;
using YAPW.MainDb;
using static System.Collections.Specialized.BitVector32;
using YAPW.MainDb.DbModels;
using EFCore.BulkExtensions;
using YAPW.Domain.Interfaces.Services.Generic;

namespace YAPW.Domain.Services.Generic
{
    public class ServiceWorker<TContext> : IServiceWorker where TContext : DataContext
    {
        #region Fields

        #region Data Context

        private TContext _context;

        #endregion Data Context

        #region Generic Entities

        private EntityRepository<MainDb.DbModels.Type, TContext> typeRepository;

        #endregion Generic Entities

        #region Generic Named Entities

        private NamedEntityRepository<MainDb.DbModels.Type, TContext> type2Repository;

        #endregion Generic Named Entities

        #region Known Entities

        //Logistics
        //private TripRepository<Trip, TContext> tripRepository;

        #endregion Known Entities

        #endregion Fields

        #region Properties

        #region Generic Entities

        //public EntityRepository<LineSpeedEventReason, TContext> LineSpeedEventReasonRepository => lineSpeedEventReasonRepository ??= new EntityRepository<LineSpeedEventReason, TContext>(_context);

        #endregion Generic Entities

        #region Named Entities

        //public NamedEntityRepository<Color, TContext> ColorRepository => colorRepository ??= new NamedEntityRepository<Color, TContext>(_context);
        
        #endregion Named Entities

        #region Known Entities

        //Logistics
        //
        //public DocumentRepository<Document, TContext> DocumentRepository => documentRepository ??= new DocumentRepository<Document, TContext>(_context);

        #endregion Known Entities

        #endregion Properties

        #region Methods

        #region Ctor

        public ServiceWorker(TContext context) => _context = context;

        #endregion Ctor

        #region Workers

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollBackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
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

        public void Dispose() => _context.Dispose();

        #endregion Workers

        #endregion Methods
    }
}
