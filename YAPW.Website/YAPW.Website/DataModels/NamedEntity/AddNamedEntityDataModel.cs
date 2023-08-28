using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.DataModels.NamedEntity
{
    /// <summary>
    /// Data model used to add a named entity
    /// </summary>
    /// <remarks>
    /// All the named entities should inherits from this one 
    /// </remarks>
    public class AddNamedEntityDataModel
    {
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Display(Name="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description{ get; set; }
    }
}
