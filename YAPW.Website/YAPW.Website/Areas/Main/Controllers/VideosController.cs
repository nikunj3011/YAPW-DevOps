using Ditech.Portal.NET.App_Base;
using Ditech.Portal.NET.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YAPW.Models;
using YAPW.Models.DataModels;
using YAPW.Website.Controllers;

namespace YAPW.Website.Areas.Main.Controllers;

[Route("[area]/[controller]")]
[Area("Main")]
public class VideosController : BaseController
{
    public AppBase appBase = new AppBase();
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMemoryCache _memoryCache;

    public VideosController(IHttpClientFactory clientFactory, IMemoryCache memoryCache) : base(clientFactory)
    {
        _memoryCache = memoryCache;
        _clientFactory = clientFactory;
        CacheData();
    }

    private async Task<(List<NamedEntityDataModel> categories, List<NamedEntityDataModel> brands)> CacheData()
    {
        //TO : DO add caching for these two
        var cacheKeyCategory = "categoryList";
        //checks if cache entries exists
        if (!_memoryCache.TryGetValue(cacheKeyCategory, out List<NamedEntityDataModel> categories))
        {
            categories = await ExecuteServiceRequest<List<NamedEntityDataModel>>(HttpMethod.Get, $"categories/all/minimal");
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
        if (!_memoryCache.TryGetValue(cacheKeyBrand, out List<NamedEntityDataModel> brands))
        {
            brands = await ExecuteServiceRequest<List<NamedEntityDataModel>>(HttpMethod.Get, $"brands/all/minimal");
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
        return (categories, brands);
    }

    [HttpGet("Index")]
    [AjaxOnly]
    public async Task<IActionResult> Index()
    {
        try
        {
            var videos = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/take/20");
            ViewBag.VideosRandom = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/random/20");
            ViewBag.Categories = await ExecuteServiceRequest<List<CategoryDataModel>>(HttpMethod.Get, $"categories/random/20");
            ViewBag.Brands = await ExecuteServiceRequest<List<BrandDataModel>>(HttpMethod.Get, $"brands/random/20");
            return PartialView(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    [AjaxOnly]
    [HttpGet]
    [Route("Name/{name}")]
    public async Task<IActionResult> Video(string name)
    {
        try
        {
            name = name.Replace("-", " ");

            var video = await ExecuteServiceRequest<VideoDataModel>(HttpMethod.Get, $"videos/detailed/" + name);
            video.VideoInfo.VideoUrl = $"https://r2.1hanime.com/{video.VideoInfo.VideoUrl}.mp4";
            //if next video is present add it
            //featured, recently addded, random, same brand
            ViewBag.VideosFeatured = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/featured/byViews/19");
            ViewBag.VideosRecentlyAdded = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/newReleases/19");
            ViewBag.VideosRandom = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/random/19");
            ViewBag.VideosSameBrand = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/brand/19/"+video.Brand.Name.ToLower());

            return PartialView("Video", video);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
	}

	[AjaxOnly]
	[HttpGet]
	[Route("Search")]
    [OutputCache(Duration = 7200)]
    public async Task<IActionResult> Search(string name, string brand = "", string category = "", Order order = Order.Descending)
	{
		try
		{
            var c = await CacheData();
            ViewBag.Brands = c.brands;
            ViewBag.Categories = c.categories;
            ViewBag.Name = name;

            ViewBag.Category = c.categories.Where(p => p.Name.ToLower() == category.ToLower())?.FirstOrDefault()?.Id;
            ViewBag.Brand = c.brands.Where(p=>p.Name.ToLower() == brand.ToLower())?.FirstOrDefault()?.Id;
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

    [AjaxOnly]
    [HttpPost]
    [Route("Post")]
    public async Task<IActionResult> SearchResult(VideoGetModel videoGetModel)
    {
        try
        {
            if (videoGetModel.Search == null)
                videoGetModel.Search = "";
            //TO : DO add caching for these two
            //ViewBag.Categories = await ExecuteServiceRequest<List<NamedEntityDataModel>>(HttpMethod.Get, $"categories/all/minimal");
            //ViewData["BrandsSelectList"] = new SelectList(ViewBag.Brands, "Id", "Name");
            //ViewData["CategoriesSelectList"] = new SelectList(ViewBag.Categories, "Id", "Name");
            return View(await ExecutePostServiceRequest<VideoSearchResponse, VideoGetModel>("videos/search", videoGetModel));
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    [HttpGet("Random")]
    [AjaxOnly]
    public async Task<IActionResult> Random()
    {
        try
        {
            var videos = await ExecuteServiceRequest<List<VideoDataModel>>(HttpMethod.Get, $"videos/random/20");
            return PartialView(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }


    [HttpGet("Brands")]
    [AjaxOnly]
    public async Task<IActionResult> Brands()
    {
        try
        {
            var videos = await ExecuteServiceRequest<List<BrandDataModel>>(HttpMethod.Get, $"brands/all/minimal");
            return PartialView(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }


    [HttpGet("Categories")]
    [AjaxOnly]
    public async Task<IActionResult> Categories()
    {
        try
        {
            var videos = await ExecuteServiceRequest<List<CategoryDataModel>>(HttpMethod.Get, $"categories/all/minimal");
            return PartialView(videos);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[AjaxOnly]
    //public async Task<IActionResult> Actor(ActorGetModel ActorGetModel)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //        var ActorRequestResult = await appBase.GetElementFromApiAsync<ActorViewModel>($"operations/Actors/erp/compact/{ActorGetModel.ErpNumber}?typeId={ActorGetModel.ActorType}", _clientFactory);
    //        if (ActorRequestResult.statusCode != HttpStatusCode.OK)
    //        {
    //            return BadRequest(ActorRequestResult.rawResponse);
    //        }
    //        var test = ActorRequestResult.returnedElement.Tanks.ActorBy(t => t.CurrentStatus);
    //        var customer = await ExecuteServiceRequest<Customer>(HttpMethod.Get, "logistics/customer/" + ActorRequestResult.returnedElement.CustomerId);
    //        ViewBag.CustomerAddress = customer.Addresses?.FirstOrDefault().Address.FullAddress;
    //        var TankStatusList = new List<TankStatuses>();
    //        if (ActorRequestResult.returnedElement.Tanks.Count() > 0)
    //        {
    //            TankStatusList.Add(new TankStatuses { TankCount = 0, status = ActorRequestResult.returnedElement.Tanks.FirstOrDefault().CurrentStatus });
    //            foreach (var tank in test)
    //            {
    //                var tankCurentStatus = tank.CurrentStatus;
    //                var exist = false;

    //                foreach (var statusList in TankStatusList)
    //                {
    //                    if (statusList.status == tankCurentStatus)
    //                    {
    //                        exist = true;
    //                        statusList.TankCount++;
    //                    }
    //                }
    //                if (!exist)
    //                {
    //                    TankStatusList.Add(new TankStatuses { status = tankCurentStatus, TankCount = 1 });
    //                }
    //            }
    //        }
    //        ViewBag.Tanks = ActorRequestResult.returnedElement.Tanks.Count;
    //        var Actor = ActorRequestResult.returnedElement;
    //        Actor.TanksStatus = TankStatusList;

    //        return View("Actor", Actor);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest("Error occured" + ex.Message);
    //    }
    //}
}
