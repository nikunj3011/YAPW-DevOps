using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
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
		private readonly IMemoryCache _memoryCache;

		public VideosController(ServiceWorker<DataContext> serviceWorker,
        NamedEntityServiceWorker<MainDb.DbModels.Video, DataContext> namedEntityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings,
		IMemoryCache memoryCache
		) : base(
            namedEntityServiceWorker,
            httpContextAccessor,
            hostingEnvironment, settings)
        {
            _serviceWorker = serviceWorker;
            _namedEntityServiceWorker = namedEntityServiceWorker;
            _repository = namedEntityServiceWorker.VideoRepository;
            _memoryCache = memoryCache;
		}

        private async Task<IEnumerable<VideoDataModel>> CacheData()
        {
            var cacheKeyVideo = "videosList";
            //checks if cache entries exists
            if (!_memoryCache.TryGetValue(cacheKeyVideo, out IEnumerable<VideoDataModel> videos))
            {
                IEnumerable<VideoDataModel> video = new List<VideoDataModel>();
				videos = await _repository.GetLimited(20000, video);
                //setting up cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(1),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromHours(1)
                };
                //setting cache entries
                _memoryCache.Set(cacheKeyVideo, videos, cacheExpiryOptions);
            }
            return videos;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> Get()
        {
            throw new Exception("Blocking to view videos for now");
        }

        [HttpGet("take/{take}")]
        public async Task<ActionResult<IEnumerable<MainDb.DbModels.Video>>> Get(int take)
        {
			//return Ok(await _repository.GetLimited(take));
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
        [HttpGet("Detailed/{linkName}")]
        public async Task<VideoDataModel> GetByNameDetailed(string linkName) => await _repository.GetByNameDetailed(linkName);

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("SearchByName/{name}")]
        public async Task<ActionResult<MainDb.DbModels.Video>> SearchByName(string name)
        {
            return Ok(await _repository.SearchVideos(name));
        }

        /// <summary>
        /// Get Types by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("VideoOfTheDay")]
        public async Task<ActionResult<VideoDataModel>> VideoOfTheDay()
        {
			return Ok(await _repository.VideoOfTheDay());
        }

		/// <summary>
		/// Post Type
		/// </summary>
		/// <param name="namedEntityDataModel"></param>
		/// <returns></returns>
		[HttpPut("updateLikes")]
		public async Task<ActionResult<MainDb.DbModels.Video>> PostLikes(Dictionary<string, int> videoViews, CancellationToken cancellationToken)
		{
			try
			{
				await _namedEntityServiceWorker.BeginTransaction();

				foreach (var item in videoViews)
				{
					var videoId = item.Key.Replace("{", "");
					videoId = videoId.Replace("}", "");
					var dbVideoInfo = await _serviceWorker.VideoInfoRepository.FindSingleAsync(p => p.VideoUrl.LinkId.ToLower() == videoId.ToLower(), null);
					Guard.Against.Null(dbVideoInfo);
					dbVideoInfo.Likes = item.Value + dbVideoInfo.Likes;
					_serviceWorker.VideoInfoRepository.Update(dbVideoInfo);
				}
				await _serviceWorker.SaveAsync();
				await _namedEntityServiceWorker.CommitTransaction();

				return Ok();
			}
			catch (Exception ex)
			{
				//catch exeption in logs or retry or send email to admin or metrics server, grafana etc
				await _namedEntityServiceWorker.RollBackTransaction();

				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Post Type
		/// </summary>
		/// <param name="namedEntityDataModel"></param>
		/// <returns></returns>
		[HttpPut("updateDislikes")]
		public async Task<ActionResult<MainDb.DbModels.Video>> PostDislikes(Dictionary<string, int> videoViews, CancellationToken cancellationToken)
		{
			try
			{
				await _namedEntityServiceWorker.BeginTransaction();

				foreach (var item in videoViews)
				{
					var videoId = item.Key.Replace("{", "");
					videoId = videoId.Replace("}", "");
					var dbVideoInfo = await _serviceWorker.VideoInfoRepository.FindSingleAsync(p => p.VideoUrl.LinkId.ToLower() == videoId.ToLower(), null);
					Guard.Against.Null(dbVideoInfo);
					dbVideoInfo.Dislikes = item.Value + dbVideoInfo.Dislikes;
					_serviceWorker.VideoInfoRepository.Update(dbVideoInfo);
				}
				await _serviceWorker.SaveAsync();
				await _namedEntityServiceWorker.CommitTransaction();

				return Ok();
			}
			catch (Exception ex)
			{
				//catch exeption in logs or retry or send email to admin or metrics server, grafana etc
				await _namedEntityServiceWorker.RollBackTransaction();

				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Post Type
		/// </summary>
		/// <param name="namedEntityDataModel"></param>
		/// <returns></returns>
		[HttpPut("updateViews")]
        public async Task<ActionResult<MainDb.DbModels.Video>> Post(Dictionary<string, int> videoViews, CancellationToken cancellationToken)
        {
            try
            {
                await _namedEntityServiceWorker.BeginTransaction();

                foreach (var item in videoViews)
                {
                    var videoId = item.Key.Replace("{", "");
                    videoId = videoId.Replace("}", "");
					var dbVideoInfo = await _serviceWorker.VideoInfoRepository.FindSingleAsync(p=>p.VideoUrl.LinkId.ToLower() == videoId.ToLower(), null);
                    Guard.Against.Null(dbVideoInfo);
                    dbVideoInfo.Views = item.Value + dbVideoInfo.Views;
                    _serviceWorker.VideoInfoRepository.Update(dbVideoInfo);
                }
                await _serviceWorker.SaveAsync();
                await _namedEntityServiceWorker.CommitTransaction();

                return Ok();
            }
            catch (Exception ex)
            {
                //catch exeption in logs or retry or send email to admin or metrics server, grafana etc
                await _namedEntityServiceWorker.RollBackTransaction();

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult<MainDb.DbModels.Video>> Post(VideoDataModel namedEntityDataModel, CancellationToken cancellationToken)
        {
            try
            {
                return BadRequest("Not supported");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Post Type
        /// </summary>
        /// <param name="namedEntityDataModel"></param>
        /// <returns></returns>
        [HttpPost("addExternal")]
        public async Task<ActionResult<MainDb.DbModels.Video>> PostVideo(AddVideoDataModel namedEntityDataModel, CancellationToken cancellationToken)
        {
            try
            {
                await _namedEntityServiceWorker.BeginTransaction();
                await _serviceWorker.VideoRepository.AddVideo(namedEntityDataModel);
                await _namedEntityServiceWorker.CommitTransaction();
                return Ok();
            }
            catch (Exception ex)
            {
                await _namedEntityServiceWorker.RollBackTransaction();
                return BadRequest(ex.Message);
            }
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