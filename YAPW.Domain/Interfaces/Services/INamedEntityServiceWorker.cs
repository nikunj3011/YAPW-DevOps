using YAPW.Domain.Repositories.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces.Services;

public interface INamedEntityServiceWorker<TNamedEntity, TContext> : IEntityServiceWorker<TNamedEntity, TContext>
    where TNamedEntity : NamedEntity
    where TContext : DataContext
{
    NamedEntityRepository<TNamedEntity, TContext> NamedEntityRepository { get; }
}
