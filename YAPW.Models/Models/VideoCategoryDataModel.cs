using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YAPW.Models
{
    public class VideoCategoryDataModel
    {
        public Guid VideoId { get; set; }

        public Guid CategoryId { get; set; }

        public CategoryDataModel Category { get; set; }
    }
}