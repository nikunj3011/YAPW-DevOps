using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class VideoCategory : EntityBase
    {
        [Required]
        [ForeignKey("Video")]
        public Guid VideoId { get; set; }

        public virtual Video Video { get; set; }

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Order { get; set; }
    }
}
