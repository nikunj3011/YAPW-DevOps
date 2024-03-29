﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        /// <summary>
        /// Time
        /// </summary>
        [Required]
        public bool IsCensored { get; set; }



        [ForeignKey("VideoUrl")]
        public Guid? VideoUrlId { get; set; }
        public virtual Link VideoUrl { get; set; }

        [ForeignKey("Cover")]
        public Guid? CoverId { get; set; }
        public virtual Link Cover { get; set; }

        [ForeignKey("Poster")]
        public Guid? PosterId { get; set; }
        public virtual Link Poster { get; set; }

        public virtual ICollection<VideoTitle> VideoTitles { get; set; }
    }

    public enum BestQuality
    {
        P480, P720, P1080, P4K
    }
}
