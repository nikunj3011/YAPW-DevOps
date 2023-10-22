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
    public class ViewsController : GenericNamedEntitiesControllerBase<MainDb.DbModels.View, DataContext, NamedEntityServiceWorker<MainDb.DbModels.View, DataContext>, NamedEntityDataModel>
    {

        private readonly NamedEntityServiceWorker<MainDb.DbModels.View, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly ViewRepository<View, DataContext> _repository;
        private readonly IMemoryCache _memoryCache;

        public ViewsController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.View, DataContext> namedEntityServiceWorker,
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
            _repository = namedEntityServiceWorker.ViewRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.View>>> Get() => await base.Get();

        /// <summary>
        /// Get Views by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.View>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Views by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.View>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Post View
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.View>> Post(NamedEntityDataModel namedEntityDataModel, CancellationToken cancellationToken)
            => await base.Post(namedEntityDataModel, cancellationToken);

		/// <summary>
		/// Update View
		/// </summary>
		/// <param name="namedEntityDataModel"></param>
		/// <returns></returns>
		[HttpGet("put")]
		public async Task Put(string name, int count, CancellationToken cancellationToken)
			=> await _repository.UpdateView(name, count);

		/// <summary>
		/// Delete View
		/// </summary>
		/// <param name="id">View Id</param>
		/// <returns></returns>
		[HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await base.Delete(id, cancellationToken);
    }
}