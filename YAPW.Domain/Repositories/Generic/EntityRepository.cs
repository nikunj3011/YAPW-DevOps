using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using YAPW.Domain.Interfaces.Repositories;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Repositories.Generic
{
    public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : EntityBase
        where TContext : DbContext
    {
        public readonly TContext context;
        private readonly DbSet<TEntity> dbSet;

        //private static readonly BulkConfig _bulkConfig = new BulkConfig
        //{
        //    SqlBulkCopyOptions = Microsoft.Data.SqlClient.SqlBulkCopyOptions.FireTriggers,
        //    SetOutputIdentity = true,
        //    PreserveInsertOrder = true
        //};

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public EntityRepository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets a list of an anonymous type based on a predicate. and a select filter.<br />
        /// <b>This method default to "no-tracking" query.</b>
        /// </summary>
        /// <remarks>
        /// The select is required .<br />
        /// This should only be used to return a type that is unknown or anonymous.<br />
        /// Please use other overloads for other type of queries
        /// </remarks>
        /// <typeparam name="TAnonymousType"></typeparam>
        /// <param name="select">An expression to build an anonymous type</param>
        /// <param name="take">How many elements should be returned from the query</param>
        /// <param name="filter">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="activeOnly"><c>true</c> to get active entities only; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="includeExtraFields">Flag to determine if we should join the extra fields linked to an entity.Default to <c>false</c></param>
        ///<exception cref="ArgumentNullException"> Thrown if the select param is null</exception>
        public virtual async Task<IEnumerable<TAnonymousType>> FindAsync<TAnonymousType>(
            Expression<Func<TEntity, TAnonymousType>> select,
            int take = 0,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool includeExtraFields = false,
            CancellationToken cancellationToken = new CancellationToken(),
            bool ignoreGlobalFilter = false,
            bool splitQuery = true) where TAnonymousType : class
        {
            ArgumentNullException.ThrowIfNull(select, nameof(select));

            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly)).AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                if (splitQuery)
                {
                    query = include(query).AsSplitQuery();
                }
                else
                {
                    query = include(query);
                }
            }

            if (ignoreGlobalFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take > 0)
            {
                return await query.Select(select).Take(take).ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            return await query.Select(select).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAsyncNoSelect(
            int take = 0,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool includeExtraFields = false,
            CancellationToken cancellationToken = new CancellationToken(),
            bool ignoreGlobalFilter = false,
            bool splitQuery = true)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly)).AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                if (splitQuery)
                {
                    query = include(query).AsSplitQuery();
                }
                else
                {
                    query = include(query);
                }
            }

            if (ignoreGlobalFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take > 0)
            {
                return await query.Take(take).ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindRandomAsyncNoSelect(
            int take = 0,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool includeExtraFields = false,
            CancellationToken cancellationToken = new CancellationToken(),
            bool ignoreGlobalFilter = false,
            bool splitQuery = true)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly)).AsNoTracking();
            Random rand = new Random();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                if (splitQuery)
                {
                    query = include(query).AsSplitQuery();
                }
                else
                {
                    query = include(query);
                }
            }

            if (ignoreGlobalFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take > 0)
            {
                int skipper = rand.Next(0, query.Count());

                return await query.OrderBy(p=> Guid.NewGuid()).Skip(skipper).Take(take).ToListAsync(cancellationToken).ConfigureAwait(false);//EF.Functions.Random()
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///  Gets the <see cref="IEnumerable{TEntity}"/> based on a predicate. This method default no-tracking query.
        /// </summary>
        /// <param name="filter">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="activeOnly"><c>true</c> to get active entities only; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <param name="trackChanges"><c>false</c> to disable changing tracking; otherwise, <c>true</c>. Default to <c>false</c>.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, bool activeOnly = true, bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool splitQuery = true,
            bool includeExtraFields = false)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly));

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                if (splitQuery)
                {
                    query = include(query).AsSplitQuery();
                }
                else
                {
                    query = include(query);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await FindAsync(filter: filter, activeOnly: true, trackChanges: false, orderBy: null, include: null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool includeExtraFields = false)
        {
            return await FindAsync(filter: filter, activeOnly: true, trackChanges: false, orderBy: orderBy, include: include, includeExtraFields: includeExtraFields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return await FindAsync(filter: filter, activeOnly: true, trackChanges: false, include: include);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="activeOnly"></param>
        /// <param name="trackChanges"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> filter, bool activeOnly = true, bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool includeExtraFields = false, bool splitQuery = true)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly));

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                if (splitQuery)
                {
                    query = include(query).AsSplitQuery();
                }
                else
                {
                    query = include(query);
                }
            }

            if (filter == null)
            {
                throw new NullReferenceException("Filter is required to retrieve single or default entities");
            }

            return await query.Where(filter).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<TAnonymousType> FindSingleAsync<TAnonymousType>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TAnonymousType>> select,
            bool activeOnly = true,
            bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool includeExtraFields = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(q => q.Active.Equals(activeOnly));

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }

            if (filter == null)
            {
                throw new NullReferenceException("Filter is required to retrieve single or default entities");
            }

            return await query.Where(filter).Select(select).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields = false)
        {
            return await FindSingleAsync(filter: filter, activeOnly: true, trackChanges: false, include: include, includeExtraFields: includeExtraFields)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false)
        {
            return await FindSingleAsync(filter: filter, activeOnly: true, trackChanges: trackChanges).ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleByIDAsync(Guid id, bool trackChanges = false)
        {
            return await FindSingleByIDAsync(id, activeOnly: true, trackChanges: trackChanges).ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleByIDAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields = false)
        {
            return await FindSingleByIDAsync(id, activeOnly: true, trackChanges: false, include: include, includeExtraFields: includeExtraFields);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activeOnly"></param>
        /// <param name="trackChanges"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleByIDAsync(Guid id, bool activeOnly = true, bool trackChanges = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool includeExtraFields = false)
        {
            return await FindSingleAsync(filter: e => e.Id == id, activeOnly: activeOnly, trackChanges: trackChanges, include: include, includeExtraFields: includeExtraFields);
        }

        /// <summary>
        /// Check if an entity exists using a filter
        /// </summary>
        /// <param name="filter">The filter used to verivy the existance of an entity</param>
        /// <param name="activeOnly">Active or non active too</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, bool activeOnly = true)
        {
            var entity = await FindSingleAsync(filter: filter, activeOnly: activeOnly).ConfigureAwait(false);
            return entity != null;
        }

        /// <summary>
        /// Check if an entity exists before adding it to the Db Set
        /// </summary>
        /// <param name="filter">Filter used to verify the existance of an entity</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public virtual async Task AddAsync(Expression<Func<TEntity, bool>> filter, TEntity entity)
        {
            if (await ExistsAsync(filter: filter))
            {
                var entityType = typeof(TEntity).Name;

                throw new Exception(string.Format($"A {entityType} with id \"{entity.Id}\" already exists."));
            }
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Add an entity to the Db Set
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Add a list of entities to the db Set
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(List<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// (Non Async) Add an entity to the dbSet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity, bool attach = true)
        {
            if (attach)
            {
                dbSet.Attach(entity);
            }

            entity.LastModificationDate = DateTime.UtcNow;
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual void BulkUpdate(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.LastModificationDate = DateTime.UtcNow;
            }

            dbSet.UpdateRange(entities);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual void AddOrUpdate(List<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isPermanent"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> filter, bool isPermanent = false, bool attach = true)
        {
            var entityType = typeof(TEntity).Name;
            var entity = await FindSingleAsync(filter: filter, trackChanges: true);

            ArgumentNullException.ThrowIfNull(entity, entityType);

            if (isPermanent)
            {
                dbSet.Remove(entity);
            }
            else
            {
                entity.Active = false;
                Update(entity, attach);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isPermanent"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TEntity entity, bool isPermanent = false, bool attach = true)
        {
            if (isPermanent)
            {
                dbSet.Remove(entity);
            }
            else
            {
                entity.Active = false;
                Update(entity, attach);
            }
        }

        // <summary>
        // Remove a list of entities.Calling the save changes is not required
        // </summary>
        // <param name = "entities" ></ param >
        // < param name="isPermanent"></param>
        // <returns></returns>
        public virtual async Task BulkRemoveAsync(List<TEntity> entities, bool isPermanent = false)
        {
            if (isPermanent)
            {
                //Uncomment this to prevent the deletion with an exception
                //
                //throw new UnauthorizedAccessException("not allowed to bulk hard delete");
                await context.BulkDeleteAsync(entities);
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.Active = false;
                }

                BulkUpdate(entities);
            }
        }
    }
}
