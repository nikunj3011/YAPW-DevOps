using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.Models.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        //Tracking fields
        //
        [Display(Name = "Is Active")]
        public bool Active { get; set; }

        //[Required]
        [Display(Name = "Creator")]
        public Guid CreatorID { get; set; }

        //public  User Creator { get; set; }

        [Required]
        [Display(Name = "Last Modification Date")]
        [DataType(DataType.DateTime)]
        public DateTime LastModificationDate { get; set; }

        [Required]
        [Display(Name = "Created date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public List<EntityField> ExtraFields { get; set; }
    }
}