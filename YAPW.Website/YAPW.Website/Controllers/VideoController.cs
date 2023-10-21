using Ditech.Portal.NET.App_Base;
using Ditech.Portal.NET.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YAPW.Models;
using YAPW.Models.DataModels;
using YAPW.Website.Controllers;

namespace YAPW.Website.Areas.Main.Controllers;

public class VideoController : BaseController
{
    public AppBase appBase = new AppBase();
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMemoryCache _memoryCache;
    private (List<CategoryDataModel> categories, List<BrandDataModel> brands, List<VideoDataModel> latestVideos, List<VideoDataModel> trendingVideos) cache;
    private Dictionary<string, int> videoLikesList =
                new Dictionary<string, int>();
    //public Task Initialization { get; private set; }
    public VideoController(IHttpClientFactory clientFactory, IMemoryCache memoryCache) : base(clientFactory)
    {
        _memoryCache = memoryCache;
        _clientFactory = clientFactory;
        //cache = CacheData();
        //Initialization = InitializeAsync();
    }

    //private async Task<VideosController> InitializeAsync()
    //{
    //    cache = await CacheData();
    //    return this;
    //}
    private async Task<(List<CategoryDataModel> categories, List<BrandDataModel> brands, List<VideoDataModel> latestVideos, List<VideoDataModel> trendingVideos)> CacheData()
    {
        var cacheKeyCategory = "categoryList";
        //checks if cache entries exists
        if (!_memoryCache.TryGetValue(cacheKeyCategory, out List<CategoryDataModel> categories))
        {
            categories = await ExecuteServiceRequest<List<CategoryDataModel>>(HttpMethod.Get, $"categories/all/minimal");
            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            //setting cache entries
            _memoryCache.Set(cacheKeyCategory, categories, cacheExpiryOptions);
        }

        var cacheKeyBrand = "brandList";
        //checks if cache entries exists
        if (!_memoryCache.TryGetValue(cacheKeyBrand, out List<BrandDataModel> brands))
        {
            brands = await ExecuteServiceRequest<List<BrandDataModel>>(HttpMethod.Get, $"brands/all/minimal");
            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            //setting cache entries
            _memoryCache.Set(cacheKeyBrand, brands, cacheExpiryOptions);
        }

        var cacheKeylatestVideos = "latestVideoList";
        if (!_memoryCache.TryGetValue(cacheKeylatestVideos, out List<VideoDataModel> latestVideos))
        {
            latestVideos = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/newReleases/20");
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            _memoryCache.Set(cacheKeylatestVideos, latestVideos, cacheExpiryOptions);
        }

        var cacheKeyTrendingVideos = "trendingVideoList";
        if (!_memoryCache.TryGetValue(cacheKeyTrendingVideos, out List<VideoDataModel> trendingVideos))
        {
            trendingVideos = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/take/20");
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            _memoryCache.Set(cacheKeyTrendingVideos, trendingVideos, cacheExpiryOptions);
        }

        return (categories, brands, latestVideos, trendingVideos);
    }

    //[AjaxOnly]
    public async Task<IActionResult> Index()
    {
        try
        {
            if(cache.categories == null)
            {
                cache = await CacheData();
            }
            var videos = cache.latestVideos;
            ViewBag.VideosTrending = cache.trendingVideos;
            ViewBag.VideosRandom = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/random/20");
            ViewBag.Categories = await ExecuteServiceRequest<List<CategoryDataModel>>(HttpMethod.Get, $"categories/random/20");
            ViewBag.Brands = await ExecuteServiceRequest<List<BrandDataModel>>(HttpMethod.Get, $"brands/random/20");
            return View(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[AjaxOnly]
    //[HttpGet("Name/{name}")]
    public async Task<IActionResult> Name(string id)
    {
        try
        {
            var video = await ExecuteServiceRequest<VideoDataModel>(HttpMethod.Get, $"Videos/detailed/" + id);
            video.VideoInfo.VideoUrl = $"https://r2.1hanime.com/{video.VideoInfo.VideoUrl}.mp4";
            //if next video is present add it
            //featured, recently addded, random, same brand
            ViewBag.VideosFeatured = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/featured/byViews/19");
            ViewBag.VideosRecentlyAdded = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/newReleases/19");
            ViewBag.VideosRandom = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/random/19");
            ViewBag.VideosSameBrand = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/brand/19/" + video.Brand.Name.ToLower());

            //if other parts exists show next or previous button
            string pattern = @"\d+$";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string trimmedName = rgx.Replace(id.Trim(), replacement);
            trimmedName = trimmedName.Replace("-", " ");

            ViewBag.VideosSameSeries = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/SearchByName/" + trimmedName);


            //update likes, dislikes, views in api
            //update when there is low user count
            //use metrics servers to get data
            //or get api metrics if low post to endpoint
            var cacheKeyVideoLikesList = "videoLikesList";

            if (!_memoryCache.TryGetValue(cacheKeyVideoLikesList, out Dictionary<string, int> cacheVideoLikesList))
            {
                cacheVideoLikesList = new Dictionary<string, int>();

                //setting up cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                }.RegisterPostEvictionCallback(async (key, value, reason, substate) =>
                {
                    //before cache is removed update those values
                    try
                    {
                        //todo later add these calls directly through servicebroker and also do this for likes and dislikes
                        var updateViews = await ExecutePutServiceRequest<string, Dictionary<string, int>>($"Videos/updateViews/", cacheVideoLikesList);
                        cacheVideoLikesList = new Dictionary<string, int>();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("lol " + ex.Message);
                    }
                });
                //setting cache entries

                _memoryCache.Set(cacheKeyVideoLikesList, cacheVideoLikesList, cacheExpiryOptions);
            }

            if (!cacheVideoLikesList.ContainsKey(id))
            {
                cacheVideoLikesList.Add(id, 1);
                videoLikesList.Add(id, 1);
            }
            else
            {
                var count = cacheVideoLikesList.GetValueOrDefault(id);
                count++;
                cacheVideoLikesList[id] = count;
            }


            return PartialView("Video", video);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("lol"))
            {
                //retry policy etc
                return BadRequest("Error occured" + ex.Message);
            }
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[AjaxOnly]
    //[HttpGet]
    //[Route("Search")]
    [OutputCache(Duration = 7200)]
    public async Task<IActionResult> Search(string name, string brand = "", string category = "", Order order = Order.Descending)
    {
        try
        {
            if (cache.categories == null)
            {
                cache = await CacheData();
            }
            ViewBag.Brands = cache.brands;
            ViewBag.Categories = cache.categories;
            ViewBag.Name = name;

            ViewBag.Category = cache.categories.Where(p => p.Name.ToLower() == category.ToLower())?.FirstOrDefault()?.Id;
            ViewBag.Brand = cache.brands.Where(p => p.Name.ToLower() == brand.ToLower())?.FirstOrDefault()?.Id;
            ViewBag.Order = order;
            //if (ViewBag.Brand == null) { ViewBag.Brand = ""; }
            //if (ViewBag.Category == null) { ViewBag.Category = ""; }
            ViewData["BrandsSelectList"] = new SelectList(ViewBag.Brands, "Id", "Name", ViewBag.Brand);
            ViewData["CategoriesSelectList"] = new SelectList(ViewBag.Categories, "Id", "Name", ViewBag.Category);
            return PartialView();
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[AjaxOnly]
    [HttpPost]
    [Route("SearchVideo")]
    public async Task<IActionResult> SearchVideo(VideoGetModel videoGetModel)
    {
        try
        {
            if (videoGetModel.Search == null)
                videoGetModel.Search = "";
            //TO : DO add caching for these two
            //ViewBag.Categories = await ExecuteServiceRequest<List<NamedEntityDataModel>>(HttpMethod.Get, $"categories/all/minimal");
            //ViewData["BrandsSelectList"] = new SelectList(ViewBag.Brands, "Id", "Name");
            //ViewData["CategoriesSelectList"] = new SelectList(ViewBag.Categories, "Id", "Name");
            return PartialView(await ExecutePostServiceRequest<VideoSearchResponse, VideoGetModel>("Videos/search", videoGetModel));
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[HttpGet("Random")]
    //[AjaxOnly]
    public async Task<IActionResult> Random()
    {
        try
        {
            var videos = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"Videos/random/20");
            return PartialView(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[HttpGet("Brands")]
    //[AjaxOnly]
    public async Task<IActionResult> Brands()
    {
        try
        {
            if (cache.categories == null)
            {
                cache = await CacheData();
            }
            return PartialView(cache.brands);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[HttpGet("Categories")]
    //[AjaxOnly]
    public async Task<IActionResult> Categories()
    {
        try
        {
            if (cache.categories == null)
            {
                cache = await CacheData();
            }
            return PartialView(cache.categories);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }
}
