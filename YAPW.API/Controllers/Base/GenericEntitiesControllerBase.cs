using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Base
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenericEntitiesControllerBase<TEntity, TContext, TServiceWorker> : ControllerBase
        where TEntity : EntityBase
        where TContext : DataContext
        where TServiceWorker : EntityServiceWorker<TEntity, TContext>
    {
        //private readonly IOptions<AppSetting> _settings;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly TServiceWorker _entityServiceWorker;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSetting> _settings;

        /// <summary>
        /// Current environment
        /// </summary>
        protected Models.Models.Settings.Environment CurrentEnvironment { get; set; }

        /// <summary>
        /// The base controller Ctor
        /// </summary>
        public GenericEntitiesControllerBase(
            TServiceWorker entityServiceWorker,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment hostingEnvironment,
            IOptions<AppSetting> settings)
        {
            _entityServiceWorker = entityServiceWorker;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _settings = settings;
            CurrentEnvironment = GetCurrentEnvironmentSettings();
        }

        /// <summary>
        ///Get entities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get() =>
            Ok(await _entityServiceWorker.EntityRepository.FindAsync().ConfigureAwait(false));

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(Guid id)
        {
            var entityTypeName = typeof(TEntity).Name;
            var entity = await _entityServiceWorker.EntityRepository.FindSingleByIDAsync(id, null);

            if (entity == null)
            {
                return NotFound($"Unable to find {entityTypeName} using id:'{id}'.");
            }

            return entity;
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _entityServiceWorker.EntityRepository.FindSingleByIDAsync(id, null);

            if (entity == null)
            {
                return NotFound($"Unable to find the {typeof(TEntity).Name}.");
            }

            await _entityServiceWorker.EntityRepository.RemoveAsync(entity);
            await _entityServiceWorker.SaveAsync();

            return NoContent();
        }

        private Models.Models.Settings.Environment GetCurrentEnvironmentSettings() =>
            _settings is not null && _hostingEnvironment is not null
            ? _settings.Value.Environments.SingleOrDefault(e => e.Name.ToLower() == _hostingEnvironment.EnvironmentName.ToLower())
            : throw new InvalidOperationException("Settings and hosting environment issues");

        #region Helpers

        #endregion Helpers
    }
}