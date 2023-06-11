using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Repositories.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Services.Generic
{
    /// <summary>
    /// Service worker used for the generic named entities.
    /// Inherits from <see cref="EntityServiceWorker"/>
    /// </summary>
    /// <typeparam name="TNamedEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class NamedEntityServiceWorker<TNamedEntity, TContext> :
        EntityServiceWorker<TNamedEntity, TContext>,
        INamedEntityServiceWorker<TNamedEntity, TContext>
        where TNamedEntity : NamedEntity
        where TContext : DataContext
    {
        #region Fields

        /// <summary>
        /// DataBase Context
        /// </summary>
        private readonly TContext _context;

        /// <summary>
        /// Private repository field
        /// </summary>
        private NamedEntityRepository<TNamedEntity, TContext> namedEntityRepository;

        #endregion Fields

        #region Properties

        public NamedEntityRepository<TNamedEntity, TContext> NamedEntityRepository => namedEntityRepository ??= new NamedEntityRepository<TNamedEntity, TContext>(_context);

        #endregion Properties

        #region Methods

        #region Constructors

        public NamedEntityServiceWorker(TContext context) : base(context) => _context = context;

        #endregion Constructors

        //Ctor

        #endregion Methods
    }
}