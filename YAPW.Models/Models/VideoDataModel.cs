using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class VideoDataModel : NamedEntityDataModel
    {
        public string LinkId { get; set; }
        public string VideoLink { get; set; }
        public string LinkName { get; set; }
    }
}