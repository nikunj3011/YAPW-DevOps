using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class VideoDataModel : NamedEntityDataModel
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public BrandDataModel Brand { get; set; }
        public List<CategoryDataModel> Categories { get; set; }
        public VideoInfoDataModel VideoInfo { get; set; }
    }
}