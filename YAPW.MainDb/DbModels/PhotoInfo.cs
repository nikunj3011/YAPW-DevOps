using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class PhotoInfo : EntityBase
    {
        /// <summary>
        /// Total likes
        /// </summary>
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

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
        public string BestQuality { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        public double VideoLength { get; set; }

    }
}
