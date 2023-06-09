using Microsoft.AspNetCore.Mvc;
using YAPW.Domain.Interfaces;
using YAPW.MainDb.DbModels;

namespace YAPW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypesController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITypeService _typeService;

        public TypesController(ITypeService typeService, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<List<MainDb.DbModels.Type>> GetAll()
        {
            return await _typeService.GetAll();
        }
    }
}