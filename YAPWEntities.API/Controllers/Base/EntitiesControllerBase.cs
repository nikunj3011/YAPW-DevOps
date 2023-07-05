using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Base;

//[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class EntitiesControllerBase : ControllerBase
{
    #region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ServiceWorker<DataContext> _serviceWorker;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IOptions<AppSetting> _settings;

    protected Models.Models.Settings.Environment CurrentEnvironment { get; set; }

    #endregion Fields

    #region Properties

    /// The Domain service worker
    /// </summary>
    public ServiceWorker<DataContext> ServiceWorker { get; set; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// The base controller Ctor
    /// </summary>
    /// <param name="serviceWorker"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="hostingEnvironment"></param>
    /// <param name="settings"></param>
    public EntitiesControllerBase(
        ServiceWorker<DataContext> serviceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment,
        IOptions<AppSetting> settings)
    {
        _httpContextAccessor = httpContextAccessor;
        _serviceWorker = serviceWorker;
        _hostingEnvironment = hostingEnvironment;
        _settings = settings;
        ServiceWorker = serviceWorker;
        CurrentEnvironment = GetCurrentEnvironmentSettings();
    }

    #endregion Constructors

    #region Methods

    private Models.Models.Settings.Environment GetCurrentEnvironmentSettings() =>
        _settings is not null && _hostingEnvironment is not null
        ? _settings.Value.Environments.SingleOrDefault(e => e.Name.ToLower() == _hostingEnvironment.EnvironmentName.ToLower())
        : throw new InvalidOperationException("Settings and hosting environment issues");
    #endregion Helpers
}
