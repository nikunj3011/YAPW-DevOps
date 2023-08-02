using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class ActorVideo : EntityBase
    {
        [Required]
        [ForeignKey("Video")]
        public Guid VideoId { get; set; }

        public virtual Video Video { get; set; }

        [Required]
        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
