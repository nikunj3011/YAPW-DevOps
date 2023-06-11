using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : EntityBase
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, bool activeOnly);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            bool activeOnly, bool trackChanges, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool splitQuery, bool includeExtraFields);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> filter, bool activeOnly, bool trackChanges,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields, bool splitQuery);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields);

        Task<TEntity> FindSingleByIDAsync(Guid id, bool trackChanges);

        Task<TEntity> FindSingleByIDAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields);

        Task<TEntity> FindSingleByIDAsync(Guid id, bool activeOnly, bool trackChanges, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool includeExtraFields);

        void Add(TEntity entity);

        Task AddAsync(Expression<Func<TEntity, bool>> filter, TEntity entity);

        Task AddAsync(List<TEntity> entities);

        void Update(TEntity entity, bool attach);

        Task RemoveAsync(Expression<Func<TEntity, bool>> filter, bool isPermanent, bool attach);

        Task<IEnumerable<TAnonymousType>> FindAsync<TAnonymousType>(Expression<Func<TEntity, TAnonymousType>> select, int take, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool activeOnly = true, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool includeExtraFields = false, CancellationToken cancellationToken = default, bool ignoreGlobalFilter = false, bool splitQuery = true) where TAnonymousType : class;

        Task<TAnonymousType> FindSingleAsync<TAnonymousType>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TAnonymousType>> select, bool activeOnly = true, bool trackChanges = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool includeExtraFields = false, CancellationToken cancellationToken = default);
        Task RemoveAsync(TEntity entity, bool isPermanent = false, bool attach = true);
    }
}