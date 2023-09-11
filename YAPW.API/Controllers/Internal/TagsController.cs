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
    [Obsolete]
    public class TagsController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Tag, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Tag, DataContext>, NamedEntityDataModel>
    {
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Tag, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly TagRepository<Tag, DataContext> _repository;

        public TagsController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Tag, DataContext> namedEntityServiceWorker,
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
            _repository = namedEntityServiceWorker.TagRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Tag>>> Get() => await base.Get();

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Tag>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Tag>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Tag>> Post(NamedEntityDataModel namedEntityDataModel, CancellationToken cancellationToken)
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