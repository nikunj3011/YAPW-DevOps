using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class VideoDataModel : NamedEntityDataModel
    {
		public Guid BrandId { get; set; }

        public LinkDataModel LinkDataModel { get; set; }

        public PhotoDataModel PhotoDataModel { get; set; }

		public string LinkId { get; set; }
		public string VideoLink { get; set; }
		public string LinkName { get; set; }
	}
}