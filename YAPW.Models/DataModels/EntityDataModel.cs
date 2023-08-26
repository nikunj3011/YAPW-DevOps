using System.ComponentModel.DataAnnotations;

namespace YAPW.Models.DataModels;

/// <summary>
/// A generic Data model for adding a named entity
/// </summary>
public class EntityDataModel
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "Last Modification Date")]
    public DateTime LastModificationDate { get; set; }

    [Required]
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; }
}
