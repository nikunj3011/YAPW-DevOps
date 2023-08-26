using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class VideoInfoVideoTitle : EntityBase
    {
        [Required]
        [ForeignKey("VideoInfo")]
        public Guid VideoInfoId { get; set; }

        public virtual VideoInfo VideoInfo { get; set; }

        [Required]
        [ForeignKey("VideoTitle")]
        public Guid VideoTitleId { get; set; }

        public virtual VideoTitle VideoTitle { get; set; }
    }
}
