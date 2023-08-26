using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class CategoryDataModel : NamedEntityDataModel
    {
        public int Count { get; set; }
        public string PhotoLink { get; set; }
    }
}