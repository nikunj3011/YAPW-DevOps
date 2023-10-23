using System.Text.Json.Serialization;

namespace YAPW.Models.Microsoft365;

public class EmailDataModel
{
	[JsonPropertyName("bccRecipients")]
	public List<string>? BccRecipients { get; set; }

	[JsonPropertyName("body")]
	public string Body { get; set; }
	public Guid? Id { get; set; }

	[JsonPropertyName("ccRecipients")]
	public List<string>? CcRecipients { get; set; }

	[JsonPropertyName("subject")]
	public string Subject { get; set; }

	[JsonPropertyName("toRecipients")]
	public List<string> ToRecipients { get; set; }
}