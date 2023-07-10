using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using YAPW.Domain.Interfaces.Repositories;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.MainDb.Interfaces;

namespace YAPW.Domain.Repositories.Generic;

public class NamedEntityRepository<TNamedEntity, TContext> : EntityRepository<TNamedEntity, TContext>, INamedEntityRepository<TNamedEntity>
    where TNamedEntity : NamedEntity, INamedEntity
    where TContext : DbContext
{
    private readonly TContext context;

    public NamedEntityRepository(TContext context) : base(context) => this.context = context;

    /// <summary>
    /// Get a single entity by Name
    /// </summary>
    /// <param name="name">The entity name</param>
    /// <param name="ignoreCase">Default to <c>True</c> in order to ignore the case of the passed string</param>
    /// <param name="includeExtraFields">Default to <c>True</c>.Determines If the extra fields should be included or not.</param>
    /// <param name="include">A predicate to pass an include lambda </param>
    /// <returns></returns>
    public async Task<TNamedEntity> FindSingleByNameAsync(string name, bool ignoreCase = true,
        bool includeExtraFields = true, Func<IQueryable<TNamedEntity>, IIncludableQueryable<TNamedEntity, object>> include = null)
    {
        return ignoreCase
            ? await FindSingleAsync(nt => nt.Name.ToLower() == name.ToLower(), include, includeExtraFields: includeExtraFields)
            : await FindSingleAsync(nt => nt.Name == name, include, includeExtraFields: includeExtraFields);
    }
}