using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models;
using YAPW.Models.DataModels;

namespace YAPW.Controllers
{
    //quartz.net
    [ApiController]
    [Route("[controller]")]
    public class TypesController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Type, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext>, NamedEntityDataModel>
    {
        private readonly EntityServiceWorker<MainDb.DbModels.Type, DataContext> _entityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;

        public TypesController(NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext> entityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment) : base(
            entityServiceWorker,
            httpContextAccessor,
            hostingEnvironment)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Type>>> Get() => await base.Get();

        /// <summary>
        /// Get Manufacturer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Type>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Manufacturer by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Type>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Post Manufacturer
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Type>> Post(NamedEntityDataModel namedEntityDataModel)
            => await base.Post(namedEntityDataModel);

        /// <summary>
        /// Delete Manufacturer
        /// </summary>
        /// <param name="id">Manufacturer Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id) => await base.Delete(id);
    }
}