using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using YAPW.Controllers.Base;
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
    public class SeedsController : EntitiesControllerBase
    { 
        private readonly NamedEntityServiceWorker<MainDb.DbModels.Actor, DataContext> _namedEntityServiceWorker;
        private readonly ServiceWorker<DataContext> _serviceWorker;
        private readonly ActorRepository<Actor, DataContext> _repository;

        public SeedsController(ServiceWorker<DataContext> serviceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings
        ) : base(
            serviceWorker,
            httpContextAccessor,
            hostingEnvironment, settings)
        {
            _serviceWorker = serviceWorker;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> GetAll()
        {
            try
            {
                var brand = new Brand
                {
                    Name = "Best Company",
                    Description = "Best of best"
                };
                await _serviceWorker.BrandRepository.AddAsync(brand);
                var category = new Category
                {
                    Name = "Funny",
                    Description = "whoa"
                };
                await _serviceWorker.CategoryRepository.AddAsync(category);

                var link = new Link
                {
                    LinkId = "https://assets.mixkit.co/videos/preview/mixkit-going-down-a-curved-highway-through-a-mountain-range-41576-large.mp4"
                };
                await _serviceWorker.LinkRepository.AddAsync(link);

                var actorProfileLink = new Link
                {
                    LinkId = "https://images.unsplash.com/photo-1554844453-7ea2a562a6c8?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHBob3RvZ3JhcGh5fGVufDB8fDB8fHww&w=1000&q=80"
                };
                await _serviceWorker.LinkRepository.AddAsync(actorProfileLink);

                var actor = new Actor
                {
                    Name = "J J Spooderman",
                    Description = "Best of best",
                    ProfilePhotoLink = actorProfileLink,
                    TotalVideos = 0
                };
                await _serviceWorker.ActorRepository.AddAsync(actor);

                var photo = new Photo
                {
                    Name = "Spooderman the boss",
                    Description = "Best of best",
                    Brand = brand,
                    Link = actorProfileLink,
                };
                await _serviceWorker.PhotoRepository.AddAsync(photo);

                var video = new Video
                {
                    Name = "Spooderman the boss",
                    Description = "Best of best",
                    Brand = brand,
                    Link = link,
                    Photo = photo
                };
                await _serviceWorker.VideoRepository.AddAsync(video);

                var videoInfo = new VideoInfo
                {
                    Video = video,
                    Likes = 100000,
                    Dislikes = 0,
                    Views = 10000,
                    ReleaseDate = DateTime.Now,
                };
                await _serviceWorker.VideoInfoRepository.AddAsync(videoInfo);

                var videoCategory = new VideoCategory
                {
                    CategoryId = category.Id,
                    VideoId  = video.Id,
                };
                await _serviceWorker.VideoCategoryRepository.AddAsync(videoCategory);
                await _serviceWorker.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }
    }
}