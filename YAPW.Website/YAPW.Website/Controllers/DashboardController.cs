using Ditech.Portal.NET.Attributes;
using Ditech.Portal.NET.Models;
using Ditech.Portal.NET.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace YAPW.Website.Controllers
{
    //[Route("/[controller]")]
    //[Route("[controller]")]
    //[Route("/",Name = "")]
    //[Area("")]
    public class DashBoardController : Controller
    {
        private readonly ILogger<DashBoardController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public DashBoardController(ILogger<DashBoardController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        //[AjaxOnly]
        public async Task<IActionResult> Index()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, "Zones");
            //var client = _clientFactory.CreateClient("api");
            //var zonesRequest = await client.SendAsync(request);

            //if (!zonesRequest.IsSuccessStatusCode)
            //{
            //    return BadRequest("unable to get zones");
            //}

            //var zones = JsonConvert.DeserializeObject<List<Zone>>(await zonesRequest.Content.ReadAsStringAsync());

            //ViewBag.Zones = zones;
            ViewData["Title"] = "Dashboard";

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjax)
            {
                return PartialView();
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy";
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return PartialView(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}