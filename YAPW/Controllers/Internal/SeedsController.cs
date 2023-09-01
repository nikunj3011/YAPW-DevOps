using Azure;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http;
using System.Text;
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

        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<NameDataModel>>> GetAll()
        //{
        //    try
        //    {
        //        var brand = new Brand
        //        {
        //            Name = "Best Company",
        //            Description = "Best of best"
        //        };
        //        await _serviceWorker.BrandRepository.AddAsync(brand);
        //        var category = new Category
        //        {
        //            Name = "Funny",
        //            Description = "whoa"
        //        };
        //        await _serviceWorker.CategoryRepository.AddAsync(category);

        //        var link = new Link
        //        {
        //            LinkId = "https://assets.mixkit.co/videos/preview/mixkit-going-down-a-curved-highway-through-a-mountain-range-41576-large.mp4"
        //        };
        //        await _serviceWorker.LinkRepository.AddAsync(link);

        //        var actorProfileLink = new Link
        //        {
        //            LinkId = "https://images.unsplash.com/photo-1554844453-7ea2a562a6c8?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHBob3RvZ3JhcGh5fGVufDB8fDB8fHww&w=1000&q=80"
        //        };
        //        await _serviceWorker.LinkRepository.AddAsync(actorProfileLink);

        //        var actor = new Actor
        //        {
        //            Name = "J J Spooderman",
        //            Description = "Best of best",
        //            ProfilePhotoLink = actorProfileLink,
        //            TotalVideos = 0
        //        };
        //        await _serviceWorker.ActorRepository.AddAsync(actor);

        //        var photo = new Photo
        //        {
        //            Name = "Spooderman the boss",
        //            Description = "Best of best",
        //            Brand = brand,
        //            Link = actorProfileLink,
        //        };
        //        await _serviceWorker.PhotoRepository.AddAsync(photo);

        //        var video = new Video
        //        {
        //            Name = "Spooderman the boss",
        //            Description = "Best of best",
        //            Brand = brand,
        //            Link = link,
        //            Photo = photo
        //        };
        //        await _serviceWorker.VideoRepository.AddAsync(video);

        //        var videoInfo = new VideoInfo
        //        {
        //            Video = video,
        //            Likes = 100000,
        //            Dislikes = 0,
        //            Views = 10000,
        //            ReleaseDate = DateTime.Now,
        //        };
        //        await _serviceWorker.VideoInfoRepository.AddAsync(videoInfo);

        //        var videoCategory = new VideoCategory
        //        {
        //            CategoryId = category.Id,
        //            VideoId  = video.Id,
        //        };
        //        await _serviceWorker.VideoCategoryRepository.AddAsync(videoCategory);
        //        await _serviceWorker.SaveAsync();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message + "  " + ex.InnerException);
        //    }
        //}

        [HttpPost("category1")]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> AddCategories()
        {
            try
            {
                string jsonCategories = System.IO.File.ReadAllText("categories.json");
                var categories = JsonConvert.DeserializeObject<List<Categories>>(jsonCategories);
                foreach (var item in categories)
                {
                    var newLink = new Link
                    {
                        LinkId = item.TallImageUrl
                    };
                    _serviceWorker.LinkRepository.Add(newLink);

                    var newCategory = new Category
                    {
                        Name = item.Text,
                        Description = item.Description,
                        PhotoUrlId = newLink.Id,
                    };
                    _serviceWorker.CategoryRepository.Add(newCategory);
                }
                await _serviceWorker.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }

        [HttpPost("brand2")]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> AddBrands()
        {
            try
            {
                string jsonBrand = System.IO.File.ReadAllText("brands.json");
                var brands = JsonConvert.DeserializeObject<List<Brands>>(jsonBrand);
                var newLink = new Link
                {
                    LinkId = "null"
                };
                _serviceWorker.LinkRepository.Add(newLink);
                foreach (var item in brands)
                {
                    var newBrand = new Brand
                    {
                        Name = item.Title,
                        Description = "~",
                        LogoId = newLink.Id,
                        WebsiteId = newLink.Id,

                    };
                    _serviceWorker.BrandRepository.Add(newBrand);
                    await _serviceWorker.SaveAsync();
                }
                await _serviceWorker.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }

        [HttpPost("video3")]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> AddVideos()
        {
            try
            {
                string jsonVideo = System.IO.File.ReadAllText("video.json");
                var videos = JsonConvert.DeserializeObject<List<Videos>>(jsonVideo);
                await _namedEntityServiceWorker.BeginTransaction();

                foreach (var item in videos)
                {
                    var videoTitles = new List<VideoTitle>();
                    foreach (var videoTitle in item.Titles)
                    {
                        var newVideoTitle = new VideoTitle
                        {
                            Name = videoTitle,
                            Description = ""
                        };
                        videoTitles.Add(newVideoTitle);
                        _serviceWorker.VideoTitleRepository.Add(newVideoTitle);
                    }

                    var posterLink = new Link
                    {
                        LinkId = item.PosterUrl
                    };
                    _serviceWorker.LinkRepository.Add(posterLink);

                    var coverLink = new Link
                    {
                        LinkId = item.CoverUrl
                    };
                    _serviceWorker.LinkRepository.Add(coverLink);

                    var videoLink = new Link
                    {
                        LinkId = item.Slug
                    };
                    _serviceWorker.LinkRepository.Add(videoLink);

                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime createdDate = start.AddSeconds(item.CreatedAt).ToLocalTime();

                    long realeaseDate = item.ReleasedAt;
                    start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime releaseDate = start.AddSeconds(item.ReleasedAt).ToLocalTime();
                    var brand = await _serviceWorker.BrandRepository.FindSingleAsync(p => p.Name.ToLower() == item.Brand.ToLower(), null);

                    var newVideo = new Video
                    {
                        Name = item.Name,
                        Description = item.Description,
                        BrandId = brand.Id,
                        VideoCategories = new List<VideoCategory>(),
                        //Link = videoLink,
                        //Photo = null
                        //VideoInfo = new VideoInfo(),
                    };
                    _serviceWorker.VideoRepository.Add(newVideo);

                    var newVideoInfo = new VideoInfo
                    {
                        Views = 0,
                        VideoTitles = videoTitles,
                        VideoUrlId = videoLink.Id,
                        PosterId = posterLink.Id,
                        CoverId = coverLink.Id,
                        VideoLength = 0,
                        IsCensored = item.IsCensored,
                        CreatedDate = createdDate,
                        ReleaseDate = releaseDate,
                        VideoId = newVideo.Id
                    };
                    _serviceWorker.VideoInfoRepository.Add(newVideoInfo);

                    var videoCateogries = new List<VideoCategory>();
                    foreach (var tag in item.Tags)
                    {
                        var category = await _serviceWorker.CategoryRepository.FindSingleAsync(p => p.Name.ToLower() == tag.ToLower(), null);
                        var newVideoCategory = new VideoCategory
                        {
                            CategoryId = category.Id,
                            //Category = category,
                            VideoId = newVideo.Id
                        };
                        _serviceWorker.VideoCategoryRepository.Add(newVideoCategory);
                        await _serviceWorker.SaveAsync();

                        videoCateogries.Add(newVideoCategory);
                    }
                    newVideo.VideoCategories = videoCateogries;
                    newVideo.VideoInfo = newVideoInfo;

                    _serviceWorker.VideoRepository.Update(newVideo);
                    await _serviceWorker.SaveAsync();
                }
                await _namedEntityServiceWorker.CommitTransaction();

                return Ok();
            }
            catch (Exception ex)
            {
                await _namedEntityServiceWorker.RollBackTransaction();
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }

        [HttpPost("video4")]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> AddVideo()
        {
            try
            {
                //string jsonVideo = System.IO.File.ReadAllText("video.json");
                var videos = await _serviceWorker.VideoRepository.FindAsync(p=>p.Name.Contains(""), null);
                foreach (var item in videos)
                {
                    item.Description = item.Description.Replace("<p>", "");
                    item.Description = item.Description.Replace("</p>", "");
                    _serviceWorker.VideoRepository.Update(item);
                }
                await _serviceWorker.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }

        [HttpPost("checkvideos")]
        public async Task<ActionResult<dynamic>> CheckVideo()
        {
            try
            {
                //string jsonVideo = System.IO.File.ReadAllText("video.json");
                var videos = await _serviceWorker.VideoRepository.FindAsyncNoSelect(include: p=>p.Include(p=>p.VideoInfo.VideoUrl));
                var invalidCount = 0;
                var validCount = 0;
                Categories categories = new Categories();
                var validVideos = new List<string>();
                var invalidVideos = new List<string>();

                foreach (var item in videos.Item1)
                {
                    string c = $"https://www.youtube.com/watch?v={item.VideoInfo.VideoUrl.LinkId}.mp4";
                    
                    bool status = categories.CheckUrlStatus(c);
                    if (status)
                    {
                        validCount++;
                        validVideos.Add(item.Name);
                    }
                    else
                    {
                        invalidCount++;
                        invalidVideos.Add(item.Name);
                    }
                }
                return Ok(invalidVideos);
                //Client.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "  " + ex.InnerException);
            }
        }
    }

    public class Categories
    {
        public bool CheckUrlStatus(string Website)
        {
            try
            {
                var request = WebRequest.Create(Website) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("wide_image_url")]
        public string WideImageUrl { get; set; }

        [JsonProperty("tall_image_url")]
        public string TallImageUrl { get; set; }
    }

    public class Brands
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("website_url")]
        public object WebsiteUrl { get; set; }

        [JsonProperty("logo_url")]
        public object LogoUrl { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public class Videos
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("titles")]
        public string[] Titles { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        [JsonProperty("interests")]
        public long Interests { get; set; }

        [JsonProperty("poster_url")]
        public string PosterUrl { get; set; }

        [JsonProperty("cover_url")]
        public string CoverUrl { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("brand_id")]
        public long BrandId { get; set; }

        [JsonProperty("duration_in_ms")]
        public long DurationInMs { get; set; }

        [JsonProperty("is_censored")]
        public bool IsCensored { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("likes")]
        public long Likes { get; set; }

        [JsonProperty("dislikes")]
        public long Dislikes { get; set; }

        [JsonProperty("downloads")]
        public long Downloads { get; set; }

        [JsonProperty("monthly_rank")]
        public long MonthlyRank { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("released_at")]
        public long ReleasedAt { get; set; }
    }


}