using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Brand : NamedEntity
    {
        public int Count { get; set; }

        [ForeignKey("Logo")]
        public Guid? LogoId { get; set; }
        public virtual Link Logo { get; set; }

        [ForeignKey("Website")]
        public Guid? WebsiteId { get; set; }
        public virtual Link Website { get; set; }
        //public List<Video> Videos { get; set; }
    }
}
