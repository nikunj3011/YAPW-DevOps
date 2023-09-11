using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
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
    public class LinksController : GenericEntitiesControllerBase<MainDb.DbModels.Link, DataContext, EntityServiceWorker<MainDb.DbModels.Link, DataContext>>
    {
        private readonly EntityServiceWorker<MainDb.DbModels.Link, DataContext> _entityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly LinkRepository<Link, DataContext> _repository;

        public LinksController(ServiceWorker<DataContext> serviceWorker,
        EntityServiceWorker<MainDb.DbModels.Link, DataContext> entityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings
        ) : base(
            entityServiceWorker,
            httpContextAccessor,
            hostingEnvironment, settings)
        {
            _serviceWorker = serviceWorker;
            _entityServiceWorker = entityServiceWorker;
            _repository = entityServiceWorker.LinkRepository;
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

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("linkId")]
        public async Task<ActionResult<MainDb.DbModels.Link>> GetByLinkId(string link)
        {
            try
            {
                return Ok(await _repository.GetByLink(link));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        public override async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await base.Delete(id, cancellationToken);
    }
}