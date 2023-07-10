using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class ActorPhoto : NamedEntity
    {
        [Required]
        [ForeignKey("Photo")]
        public Guid PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        [Required]
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
