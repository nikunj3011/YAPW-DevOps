using YAPW.Models.DataModels;

namespace YAPW.Models
{
	public class PhotoDataModel : NamedEntityDataModel
	{
		public Guid BrandId { get; set; }

		public LinkDataModel LinkDataModel { get; set; }
	}
}