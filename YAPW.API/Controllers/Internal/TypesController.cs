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
    public class TypesController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Type, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext>, NamedEntityDataModel>
    {
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly TypeRepository<MainDb.DbModels.Type, DataContext> _repository;

        public TypesController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext> namedEntityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings
        ) : base(
            namedEntityServiceWorker,
            httpContextAccessor,
            hostingEnvironment, settings)
        {
            _serviceWorker = serviceWorker;
            _namedEntityServiceWorker = namedEntityServiceWorker;
            _repository = namedEntityServiceWorker.TypeRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Type>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Type>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Type>> GetByName(string name) => await base.GetByName(name);


        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("SearchByName/{name}")]
        public async Task<ActionResult<MainDb.DbModels.Type>> SearchByName(string name, int take)
        {
            return Ok(await _namedEntityServiceWorker.TypeRepository.SearchTypes(name, take));
        }

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Type>> Post(NamedEntityDataModel namedEntityDataModel, CancellationToken cancellationToken)
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