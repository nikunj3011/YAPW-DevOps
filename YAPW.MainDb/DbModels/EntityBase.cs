using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YAPW.MainDb.DbModels
{
    public class EntityBase
    {
        protected EntityBase()
        {
            CreatedDate = DateTime.UtcNow;
            LastModificationDate = DateTime.UtcNow;
            Active = true;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        //Tracking fields
        //
        [Required]
        [Display(Name = "Is Active")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Last Modification Date")]
        [DataType(DataType.DateTime)]
        public DateTime LastModificationDate { get; set; }

        [Required]
        [Display(Name = "Created date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
