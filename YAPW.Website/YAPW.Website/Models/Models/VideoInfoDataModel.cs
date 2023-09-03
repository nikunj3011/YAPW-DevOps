using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace YAPW.Models
{
    public class VideoInfoDataModel
    {
        /// <summary>
        /// Total likes
        /// </summary>
        [Required]
        public Guid VideoId { get; set; }

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
        //public BestQuality BestQuality { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [Required]
        public double VideoLength { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [Required]
        public bool IsCensored { get; set; }

        public Guid? VideoUrlId { get; set; }
        public string VideoUrl { get; set; }

        public Guid? CoverId { get; set; }
        public string Cover { get; set; }

        public Guid? PosterId { get; set; }
        public string Poster { get; set; }

        public List<string> VideoTitles { get; set; }
    }
}