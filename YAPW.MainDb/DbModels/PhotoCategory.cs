using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class PhotoCategory : EntityBase
    {
        [Required]
        [ForeignKey("Photo")]
        public Guid PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Order { get; set; }
        //public List<Video> Videos { get; set; }
    }
}
