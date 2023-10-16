using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
using YAPW.Domain.Interfaces.Services.Generic;
using YAPW.Domain.Repositories.Main;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models.DataModels;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Internal
{
    //quartz.net
    [ApiController]
    [Route("[controller]")]
    public class BrandsController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Brand, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Brand, DataContext>, NamedEntityDataModel>
    {

        private readonly NamedEntityServiceWorker<MainDb.DbModels.Brand, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly BrandRepository<Brand, DataContext> _repository;
        private readonly IMemoryCache _memoryCache;

        public BrandsController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Brand, DataContext> namedEntityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings,
        IMemoryCache memoryCache
        ) : base(
            namedEntityServiceWorker,
            httpContextAccessor,
            hostingEnvironment,
            settings)
        {
            _memoryCache = memoryCache;
            _serviceWorker = serviceWorker;
            _namedEntityServiceWorker = namedEntityServiceWorker;
            _repository = namedEntityServiceWorker.BrandRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Brand>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Brand>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Brand>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Get Random category by take
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("random/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Category>>> GetRandom(int take)
        {
            return Ok(await _repository.GetRandomLimited(take));
        }

        [HttpGet("all/minimal")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAllMinimal() => Ok(await _repository.GetAllMinimal(_memoryCache));

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Brand>> Post(NamedEntityDataModel namedEntityDataModel, CancellationToken cancellationToken)
            => await base.Post(namedEntityDataModel, cancellationToken);

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await base.Delete(id, cancellationToken);
    }
}