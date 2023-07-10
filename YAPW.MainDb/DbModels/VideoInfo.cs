using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class VideoInfo : EntityBase
    {
        /// <summary>
        /// Total likes
        /// </summary>
        [Required]
        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }

        /// <summary>
        /// Total likes
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Total Dislikes
        /// </summary>
        public int Dislikes { get; set; }

        /// <summary>
        /// Total views
        /// </summary>
        public int Views { get; set; }

        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 720p, 1080, 4k
        /// </summary>
        public BestQuality BestQuality { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [Required]
        public double VideoLength { get; set; }

    }

    public enum BestQuality
    {
        P480, P720, P1080, P4K
    }
}
