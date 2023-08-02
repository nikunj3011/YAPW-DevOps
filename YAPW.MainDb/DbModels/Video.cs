using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Video : NamedEntity
    {
        /// <summary>
        /// all actors
        /// </summary>
        public virtual ICollection<ActorVideo> ActorVideos { get; set; }

        /// <summary>
        /// Brand ~ Brazzers, Fidelity etc
        /// </summary>
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Categories ~ Teen, HD, 4K, Hotel
        /// </summary>
        //public virtual List<Category> Categories { get; set; }
        public virtual ICollection<VideoCategory> VideoCategories { get; set; }

        /// <summary>
        /// AWS, azure or gcp link?
        /// </summary>
        public Guid LinkId { get; set; }
        public virtual Link Link { get; set; }

        public virtual VideoInfo VideoInfo { get; set; }

        /// <summary>
        /// </summary>
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

    }
}
