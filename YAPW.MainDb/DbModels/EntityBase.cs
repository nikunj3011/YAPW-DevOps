using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YAPW.MainDb.Interfaces;

namespace YAPW.MainDb.DbModels
{
    public class EntityBase : IEntityBase
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
        //public required DateTime CreatedDate { get; set; }
    }
}
