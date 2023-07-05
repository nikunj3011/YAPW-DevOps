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
    public class LinksController : GenericEntitiesControllerBase<MainDb.DbModels.Link, DataContext, EntityServiceWorker<MainDb.DbModels.Link, DataContext>>
    {
        private readonly EntityServiceWorker<MainDb.DbModels.Link, DataContext> _entityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;

        public LinksController(EntityServiceWorker<MainDb.DbModels.Link, DataContext> entityServiceWorker,
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
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Link>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Link>> GetById(Guid id) => await base.GetById(id);

        ///// <summary>
        ///// Post Type
        ///// </summary>
        ///// <param name="namedEntityDataModel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public override async Task<ActionResult<MainDb.DbModels.Link>> Post(NamedEntityDataModel namedEntityDataModel)
        //    => await base.Post(namedEntityDataModel);

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id) => await base.Delete(id);
    }
}