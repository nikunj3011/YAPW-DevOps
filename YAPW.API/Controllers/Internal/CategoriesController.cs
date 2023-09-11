using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RTools_NTS.Util;
using YAPW.Controllers.Base;
using YAPW.Domain.Repositories.Main;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models;
using YAPW.Models.DataModels;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Internal
{
    //quartz.net
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Category, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Category, DataContext>, NamedEntityDataModel>
    {
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Category, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly CategoryRepository<Category, DataContext> _repository;
        private readonly IOutputCacheStore _cache;

        public CategoriesController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Category, DataContext> namedEntityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings,
        IOutputCacheStore cache
        ) : base(
            namedEntityServiceWorker,
            httpContextAccessor,
            hostingEnvironment,
            settings)
        {
            _cache = cache;
            _serviceWorker = serviceWorker;
            _namedEntityServiceWorker = namedEntityServiceWorker;
            _repository = namedEntityServiceWorker.CategoryRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Category>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Category>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Category>> GetByName(string name) => await base.GetByName(name);

        [HttpGet("all/minimal")]
        [OutputCache(PolicyName = "CategoyMinimalPolicy")]
        public async Task<ActionResult<IEnumerable<CategoryDataModel>>> GetAllMinimal()
        {
            return Ok(await _repository.GetAllMinimal());
        }

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

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        [OutputCache(PolicyName = "CategoyMinimalPolicy")]
        public override async Task<ActionResult<MainDb.DbModels.Category>> Post(NamedEntityDataModel namedEntityDataModel, CancellationToken token)
        {
            await _cache.EvictByTagAsync("CategoyMinimalPolicy_Tag", token);
            return await base.Post(namedEntityDataModel, token);
        }

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [OutputCache(PolicyName = "CategoyMinimalPolicy")]
        public override async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            await _cache.EvictByTagAsync("CategoyMinimalPolicy_Tag", token);
            return await base.Delete(id, token);
        }
    }
}