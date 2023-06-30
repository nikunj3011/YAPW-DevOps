using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using YAPW.Controllers.Base;
using YAPW.Domain.App_Base;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models;
using YAPW.Models.DataModels;

namespace YAPW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController : ControllerBase
    {
        private AppBase appBase;
        private IHttpClientFactory _httpClientFactory;
        private readonly INameService _nameService;

        public NamesController(IHttpClientFactory httpClientFactory, INameService nameService)
        {
            appBase = new AppBase();
            _httpClientFactory = httpClientFactory;
            _nameService = nameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> GetAll()
        {
            try
            {
                return Ok(await _nameService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}