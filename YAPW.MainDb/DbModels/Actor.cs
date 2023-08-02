using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Actor : NamedEntity
    {
        [ForeignKey("Link")]
        public Guid LinkId { get; set; }
        public virtual Link ProfilePhotoLink { get; set; }

        public int TotalVideos { get; set; }
        //public string TotalVideos { get; set; }
    }
}
