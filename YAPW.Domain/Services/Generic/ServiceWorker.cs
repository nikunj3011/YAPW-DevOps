using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Interfaces.Services.Generic;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Repositories.Main;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Services.Generic
{
    public class ServiceWorker<TContext> : IServiceWorker where TContext : DbContext
    {
        #region Fields

        #region Data Context

        private TContext _context;

        #endregion Data Context

        #region Generic Entities

        //private EntityRepository<Link, TContext> linkRepository;
        private LinkRepository<Link, TContext> linkRepository;
        private EntityRepository<VideoCategory, TContext> videoCategoryRepository;
        private EntityRepository<PhotoCategory, TContext> photoCategoryRepository;
        private EntityRepository<VideoInfo, TContext> videoInfoRepository;
        private EntityRepository<VideoInfoVideoTitle, TContext> videoInfoVideoTitleRepository;

        #endregion Generic Entities

        #region Generic Named Entities

        //private NamedEntityRepository<Brand, TContext> brandRepository;
        //private NamedEntityRepository<Category, TContext> categoryRepository;
        //private NamedEntityRepository<Photo, TContext> photoRepository;
        //private NamedEntityRepository<Tag, TContext> tagRepository;
        //private NamedEntityRepository<MainDb.DbModels.Type, TContext> typeRepository;
        //private NamedEntityRepository<Video, TContext> videoRepository;
        private NamedEntityRepository<VideoTitle, TContext> videoTitleRepository;

        #endregion Generic Named Entities

        #region Known Entities

        private BrandRepository<Brand, TContext> brandRepository;
        private CategoryRepository<Category, TContext> categoryRepository;
        private PhotoRepository<Photo, TContext> photoRepository;
        private ActorRepository<Actor, TContext> actorRepository;
        private TagRepository<Tag, TContext> tagRepository;
        private TypeRepository<MainDb.DbModels.Type, TContext> typeRepository;
        private VideoRepository<Video, TContext> videoRepository;

        #endregion Known Entities

        #endregion Fields

        #region Properties

        #region Generic Entities

        public LinkRepository<Link, TContext> LinkRepository => linkRepository ??= new LinkRepository<Link, TContext>(_context);
        public EntityRepository<VideoCategory, TContext> VideoCategoryRepository => videoCategoryRepository ??= new EntityRepository<VideoCategory, TContext>(_context);
        public EntityRepository<PhotoCategory, TContext> PhotoCategoryRepository => photoCategoryRepository ??= new EntityRepository<PhotoCategory, TContext>(_context);
        public EntityRepository<VideoInfo, TContext> VideoInfoRepository => videoInfoRepository ??= new EntityRepository<VideoInfo, TContext>(_context);
        public EntityRepository<VideoInfoVideoTitle, TContext> VideoInfoVideoTitleRepository => videoInfoVideoTitleRepository ??= new EntityRepository<VideoInfoVideoTitle, TContext>(_context);

        #endregion Generic Entities

        #region Named Entities

        //public NamedEntityRepository<Brand, TContext> BrandRepository => brandRepository ??= new NamedEntityRepository<Brand, TContext>(_context);
        //public NamedEntityRepository<Category, TContext> CategoryRepository => categoryRepository ??= new NamedEntityRepository<Category, TContext>(_context);
        //public NamedEntityRepository<Photo, TContext> PhotoRepository => photoRepository ??= new NamedEntityRepository<Photo, TContext>(_context);
        //public NamedEntityRepository<Tag, TContext> TagRepository => tagRepository ??= new NamedEntityRepository<Tag, TContext>(_context);
        //public NamedEntityRepository<MainDb.DbModels.Type, TContext> TypeRepository => typeRepository ??= new NamedEntityRepository<MainDb.DbModels.Type, TContext>(_context);
        //public NamedEntityRepository<Video, TContext> VideoRepository => videoRepository ??= new NamedEntityRepository<Video, TContext>(_context);
        public NamedEntityRepository<VideoTitle, TContext> VideoTitleRepository => videoTitleRepository ??= new NamedEntityRepository<VideoTitle, TContext>(_context);

        #endregion Named Entities

        #region Known Entities

        //public DocumentRepository<Document, TContext> DocumentRepository => documentRepository ??= new DocumentRepository<Document, TContext>(_context);
        public BrandRepository<Brand, TContext> BrandRepository => brandRepository ??= new BrandRepository<Brand, TContext>(_context);
        public CategoryRepository<Category, TContext> CategoryRepository => categoryRepository ??= new CategoryRepository<Category, TContext>(_context);
        public PhotoRepository<Photo, TContext> PhotoRepository => photoRepository ??= new PhotoRepository<Photo, TContext>(_context);
        public ActorRepository<Actor, TContext> ActorRepository => actorRepository ??= new ActorRepository<Actor, TContext>(_context);
        public TagRepository<Tag, TContext> TagRepository => tagRepository ??= new TagRepository<Tag, TContext>(_context);
        public TypeRepository<MainDb.DbModels.Type, TContext> TypeRepository => typeRepository ??= new TypeRepository<MainDb.DbModels.Type, TContext>(_context);
        public VideoRepository<Video, TContext> VideoRepository => videoRepository ??= new VideoRepository<Video, TContext>(_context);
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
