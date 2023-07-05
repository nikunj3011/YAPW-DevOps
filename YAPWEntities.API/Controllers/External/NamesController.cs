using Microsoft.AspNetCore.Mvc;
using YAPW.Domain.Interfaces.External;
using YAPW.Models;

namespace YAPW.Controllers.External
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly INameService _nameService;

        public NamesController(IHttpClientFactory httpClientFactory, INameService nameService)
        {
            _httpClientFactory = httpClientFactory;
            _nameService = nameService;
        }

        //    : EntitiesControllerBase
        //{
        //    private IHttpClientFactory _httpClientFactory;
        //    private readonly INameService _nameService;
        //    private ServiceWorker<DataContext> _serviceWorker;

        //    public NamesController(IHttpClientFactory httpClientFactory, INameService nameService, ServiceWorker<DataContext> serviceWorker, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, IOptions<AppSetting> settings) : base(serviceWorker, httpContextAccessor, hostingEnvironment, settings)
        //    {
        //        _httpClientFactory = httpClientFactory;
        //        _nameService = nameService;
        //        _serviceWorker = serviceWorker;
        //    }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NameDataModel>>> GetAll()
        {
            try
            {
                return Ok(await _nameService.GetAll(_httpClientFactory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}