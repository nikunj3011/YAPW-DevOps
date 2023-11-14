using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class BrandDataModel : NamedEntityDataModel
    {
        public string? PhotoLink { get; set; }
        public int Count { get; set; }
    }
}