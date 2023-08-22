using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Category : NamedEntity
    {
        public int Count { get; set; }

        [ForeignKey("Link")]
        public Guid PhotoUrlId { get; set; }
        public virtual Link PhotoUrl { get; set; }
        //public List<Video> Videos { get; set; }
    }
}
