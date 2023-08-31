using System;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.Models.Base
{
    public class EntityField : EntityBase
    {
        /// </summary>
        [Display(Name = "Entity Name")]
        [DataType(DataType.Text)]
        [Required]
        public string EntityName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [Display(Name = "Field")]
        public Guid FieldId { get; set; }

        public Field Field { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [Display(Name = "Entity")]
        //[ForeignKey("EntityBase")]
        public Guid EntityId { get; set; }
    }
}
