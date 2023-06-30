using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;

namespace YAPW.Controllers.Base;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class EntitiesControllerBase : ControllerBase
{
    #region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ServiceWorker<DataContext> _serviceWorker;
    private readonly IWebHostEnvironment _hostingEnvironment;

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
    /// <param name="identityServiceWorker"></param>
    /// <param name="appUserManager"></param>
    /// <param name="appRoleManager"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="hostingEnvironment"></param>
    /// <param name="settings"></param>
    public EntitiesControllerBase(
        ServiceWorker<DataContext> serviceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _serviceWorker = serviceWorker;
        _hostingEnvironment = hostingEnvironment;

        ServiceWorker = serviceWorker;
    }

    #endregion Constructors

    #region Methods

    #endregion Helpers
}
