using System.ComponentModel.DataAnnotations;

namespace YAPW.Models.DataModels;

/// <summary>
/// A generic Data model for adding a named entity
/// </summary>
public class NamedEntityDataModel : EntityDataModel
{
    /// <summary>
    /// The entity name
    /// </summary>
    [Display(Name = "Name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// The entity description
    /// </summary>
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
}
