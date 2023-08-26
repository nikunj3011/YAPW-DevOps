using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Photo : NamedEntity
    {
        /// <summary>
        /// all actors
        /// </summary>
        public virtual ICollection<ActorPhoto> Actors { get; set; }

        /// <summary>
        /// </summary>
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// </summary>
        public virtual ICollection<PhotoCategory> PhotoCategories { get; set; }

        /// <summary>
        /// AWS, azure or gcp link?
        /// </summary>
        public Guid LinkId { get; set; }
        public virtual Link Link { get; set; }

        public virtual PhotoInfo PhotoInfo { get; set; }

    }
}
