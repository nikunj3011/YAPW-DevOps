using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class View : NamedEntity
    {
        [Required]
        [Display(Name = "Type")]
        [ForeignKey("Type")]
        public Guid TypeId { get; set; }

        public virtual Type Type { get; set; }

        public int TotalViews { get; set; }
    }
}
