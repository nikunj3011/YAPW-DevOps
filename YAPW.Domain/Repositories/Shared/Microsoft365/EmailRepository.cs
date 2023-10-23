using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using YAPW.Domain.Interfaces.Shared.Microsoft365;
using YAPW.Models.Microsoft365;

namespace YAPW.Domain.Repositories.Shared.Microsoft365;

public class EmailRepository : IEmailService
{
	private readonly IConfiguration _config;

	public EmailRepository(IConfiguration config)
	{
		_config = config;
	}

	public async Task SendEmail(EmailDataModel message, string fromAddress, string htmlContent)
	{
		string? tenantId = _config["Microsft365Settings:tenantId"];
		string? clientId = _config["Microsft365Settings:clientId"];
		string? clientSecret = _config["Microsft365Settings:clientSecret"];
		ClientSecretCredential credential = new(tenantId, clientId, clientSecret);
		GraphServiceClient graphClient = new(credential);

		Message email = new Message
		{
			Subject = message.Subject,
			Body = new ItemBody { Content = string.IsNullOrWhiteSpace(htmlContent) ? message.Body : htmlContent, ContentType = BodyType.Html },
			From = new Recipient { EmailAddress = new EmailAddress { Address = fromAddress } },
			ToRecipients = new List<Recipient>(),
			CcRecipients = new List<Recipient>(),
			BccRecipients = new List<Recipient>()
		};

		var toRecipients = new List<Recipient>();
		var ccRecipients = new List<Recipient>();
		var bccRecipients = new List<Recipient>();
		if (message.ToRecipients != null)
		{
			foreach (var item in message.ToRecipients)
			{
				var toRecipient = new Recipient();
				toRecipient.EmailAddress = new EmailAddress { Address = item };
				toRecipients.Add(toRecipient);
			}
		}

		if (message.CcRecipients != null)
		{
			foreach (var item in message.CcRecipients)
			{
				var ccRecipient = new Recipient();
				ccRecipient.EmailAddress = new EmailAddress { Address = item };
				ccRecipients.Add(ccRecipient);
			}
		}

		if (message.BccRecipients != null)
		{
			foreach (var item in message.BccRecipients)
			{
				var bccRecipient = new Recipient();
				bccRecipient.EmailAddress = new EmailAddress { Address = item };
				bccRecipients.Add(bccRecipient);
			}
		}

		email.ToRecipients = toRecipients;
		email.CcRecipients = ccRecipients;
		email.BccRecipients = ccRecipients;

		var body = new SendMailPostRequestBody()
		{
			Message = email,
			SaveToSentItems = true
		};

		await graphClient.Users["the@nikunjrathod3011gmail.onmicrosoft.com"]
			//await graphClient.Users["nikunjrathod3011gmail.com"]
			.SendMail
			.PostAsync(body).ConfigureAwait(false);
	}
}
