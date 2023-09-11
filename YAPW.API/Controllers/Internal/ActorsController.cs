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
    public class ActorsController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Actor, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Actor, DataContext>, NamedEntityDataModel>
    {
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Actor, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly ActorRepository<Actor, DataContext> _repository;

        public ActorsController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Actor, DataContext> namedEntityServiceWorker,
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
            _repository = namedEntityServiceWorker.ActorRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Actor>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Actor>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Actor>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost("2")]
        public async Task<ActionResult<MainDb.DbModels.Actor>> Post2(NamedEntityDataModel namedEntityDataModel)
            //=> await base.Post(namedEntityDataModel);
        {
            await _repository.AddActor(namedEntityDataModel.Name);
            return Ok();
        }

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await base.Delete(id, cancellationToken);
    }
}