using Microsoft.EntityFrameworkCore.Query;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces.Repositories
{
    public interface INamedEntityRepository<TNamedEntity> : IEntityRepository<TNamedEntity> where TNamedEntity : NamedEntity
    {
        Task<TNamedEntity> FindSingleByNameAsync(string name, bool ignoreCase = true, bool includeExtraFields = true, Func<IQueryable<TNamedEntity>, IIncludableQueryable<TNamedEntity, object>> include = null);
    }
}