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
        /// all pornstars
        /// </summary>
        public List<Pornstar> Pornstars { get; set; }

        /// <summary>
        /// Brand ~ Brazzers, Fidelity etc
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// Categories ~ Teen, HD, 4K, Hotel
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// AWS, azure or gcp link?
        /// </summary>
        public Link Link { get; set; }

    }
}
