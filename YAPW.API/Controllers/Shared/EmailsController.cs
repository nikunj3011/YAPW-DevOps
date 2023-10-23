using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YAPW.Controllers.Base;
using YAPW.Domain.Interfaces.Services;
using YAPW.Domain.Interfaces.Services.Generic;
using YAPW.Domain.Interfaces.Shared.Microsoft365;
using YAPW.Domain.Repositories.Main;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models.DataModels;
using YAPW.Models.Microsoft365;
using YAPW.Models.Models.Settings;

namespace YAPW.Controllers.Internal
{
	//quartz.net
	[ApiController]
	[Route("[controller]")]
	public class EmailsController : EntitiesControllerBase
	{
		private readonly ServiceWorker<DataContext> _serviceWorker;
		private readonly IEmailService _emailService;

		public EmailsController(ServiceWorker<DataContext> serviceWorker,
        IEmailService emailService,
		IHttpContextAccessor httpContextAccessor,
		IWebHostEnvironment hostingEnvironment,
		IOptions<AppSetting> settings
		) : base(
			serviceWorker,
			httpContextAccessor,
			hostingEnvironment, settings)
		{
            _emailService = emailService;
			_serviceWorker = serviceWorker;
		}

		/// <summary>
		/// Send Email to any user
		/// Wont work as it needs microsoft 365 account
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Get(EmailDataModel emailDataModel)
		{
            await _emailService.SendEmail(emailDataModel, "nikunjrathod3011@gmail.com", "");
			return Ok();
		}
	}
}