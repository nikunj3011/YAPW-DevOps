using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
using YAPW.Domain.Interfaces.Services;
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
    public class VideosController : GenericNamedEntitiesControllerBase<MainDb.DbModels.Video, DataContext, NamedEntityServiceWorker<MainDb.DbModels.Video, DataContext>, VideoDataModel>
    {
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Video, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly VideoRepository<Video, DataContext> _repository;

        public VideosController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Video, DataContext> namedEntityServiceWorker,
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
            _repository = namedEntityServiceWorker.VideoRepository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> Get() => await base.Get();

        [HttpGet("take/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> Get(int take)
        {
            return Ok(await _repository.GetLimited(take));
        }

        [HttpGet("random/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> GetRandom(int take)
        {
            return Ok(await _repository.GetRandomLimited(take));
        }

        [HttpGet("brand/{take}/{brandName}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> GetRandomByBrand(string brandName, int take)
        {
            return Ok(await _repository.GetRandomLimitedByBrand(brandName, take));
        }

        [HttpGet("newReleases/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> GetByReleaseDate(int take)
        {
            return Ok(await _repository.GetLimitedByReleaseDate(take));
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<dynamic>>> SearchWithPagination(VideoGetModel videoGetModel)
        {
            return Ok(await _repository.SearchWithPagination(videoGetModel));
        }

        [HttpGet("featured/byViews/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> GetByViews(int take)
        {
            return Ok(await _repository.GetLimitedByViews(take));
        }

        /// <summary>
        /// Get Types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<MainDb.DbModels.Video>> GetById(Guid id) => await base.GetById(id);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ByName/{name}")]
        public override async Task<ActionResult<MainDb.DbModels.Video>> GetByName(string name) => await base.GetByName(name);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Detailed/{name}")]
        public async Task<VideoDataModel> GetByNameDetailed(string name) => await _repository.GetByNameDetailed(name);
       
        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("SearchByName/{name}")]
        public async Task<ActionResult<MainDb.DbModels.Video>> SearchByName(string name, int take)
        {
            return Ok(await _namedEntityServiceWorker.TypeRepository.SearchTypes(name, take));
        }

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Video>> Post(VideoDataModel namedEntityDataModel)
        {
            try
            {
				var video = new Video
				{
					Name = namedEntityDataModel?.Name,
					Description = namedEntityDataModel?.Description,
					BrandId = namedEntityDataModel.BrandId,
					//Link = new Link
     //               {
     //                   LinkId = namedEntityDataModel?.LinkDataModel?.LinkId
     //               },
					//Photo = new Photo
     //               {
     //                   Name = namedEntityDataModel?.PhotoDataModel?.Name,
     //                   Description = namedEntityDataModel?.PhotoDataModel?.Description,
     //                   BrandId = namedEntityDataModel.PhotoDataModel.BrandId,
					//	Link = new Link
					//	{
					//		LinkId = namedEntityDataModel?.PhotoDataModel?.LinkDataModel?.LinkId
					//	},
					//}
				};
				await _serviceWorker.VideoRepository.AddAsync(video);
				await _serviceWorker.SaveAsync();
                return Ok(video);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id) => await base.Delete(id);
    }
}