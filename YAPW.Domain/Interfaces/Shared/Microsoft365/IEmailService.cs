using YAPW.Models.Microsoft365;

namespace YAPW.Domain.Interfaces.Shared.Microsoft365;

public interface IEmailService
{
	Task SendEmail(EmailDataModel message, string fromEmail, string htmlContent);
}