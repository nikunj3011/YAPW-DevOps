using Azure;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Repositories.Main;
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

        private BrandRepository<MainDb.DbModels.Brand, TContext> brandRepository;
        private CategoryRepository<MainDb.DbModels.Category, TContext> categoryRepository;
        private PhotoRepository<MainDb.DbModels.Photo, TContext> photoRepository;
        private ActorRepository<MainDb.DbModels.Actor, TContext> actorRepository;
        private TagRepository<MainDb.DbModels.Tag, TContext> tagRepository;
        private TypeRepository<MainDb.DbModels.Type, TContext> typeRepository;
        private VideoRepository<MainDb.DbModels.Video, TContext> videoRepository;
        private ViewRepository<MainDb.DbModels.View, TContext> viewRepository;

        #endregion Fields

        #region Properties

        public NamedEntityRepository<TNamedEntity, TContext> NamedEntityRepository => namedEntityRepository ??= new NamedEntityRepository<TNamedEntity, TContext>(_context);
        public BrandRepository<MainDb.DbModels.Brand, TContext> BrandRepository => brandRepository ??= new BrandRepository<MainDb.DbModels.Brand, TContext>(_context);
        public CategoryRepository<MainDb.DbModels.Category, TContext> CategoryRepository => categoryRepository ??= new CategoryRepository<MainDb.DbModels.Category, TContext>(_context);
        public PhotoRepository<MainDb.DbModels.Photo, TContext> PhotoRepository => photoRepository ??= new PhotoRepository<MainDb.DbModels.Photo, TContext>(_context);
        public ActorRepository<MainDb.DbModels.Actor, TContext> ActorRepository => actorRepository ??= new ActorRepository<MainDb.DbModels.Actor, TContext>(_context);
        public TagRepository<MainDb.DbModels.Tag, TContext> TagRepository => tagRepository ??= new TagRepository<MainDb.DbModels.Tag, TContext>(_context);
        public TypeRepository<MainDb.DbModels.Type, TContext> TypeRepository => typeRepository ??= new TypeRepository<MainDb.DbModels.Type, TContext>(_context);
        public VideoRepository<MainDb.DbModels.Video, TContext> VideoRepository => videoRepository ??= new VideoRepository<MainDb.DbModels.Video, TContext>(_context);
        public ViewRepository<MainDb.DbModels.View, TContext> ViewRepository => viewRepository ??= new ViewRepository<MainDb.DbModels.View, TContext>(_context);

        #endregion Properties

        #region Methods

        #region Constructors

        public NamedEntityServiceWorker(TContext context) : base(context) => _context = context;

        #endregion Constructors

        //Ctor

        #endregion Methods
    }
}