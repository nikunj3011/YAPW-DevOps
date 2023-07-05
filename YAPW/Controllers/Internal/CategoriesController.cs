using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.Models.DataModels;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Internal
{
    //quartz.net
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Category, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Category, DataContext>, NamedEntityDataModel>
    {
        private readonly EntityServiceWorker<MainDb.DbModels.Category, DataContext> _entityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;

        public CategoriesController(NamedEntityServiceWorker<MainDb.DbModels.Category, DataContext> entityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings
        ) : base(
            entityServiceWorker,
            httpContextAccessor,
            hostingEnvironment)
        {
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

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Category>> Post(NamedEntityDataModel namedEntityDataModel)
            => await base.Post(namedEntityDataModel);

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id) => await base.Delete(id);
    }
}