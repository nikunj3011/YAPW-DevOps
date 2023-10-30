using System.ComponentModel.DataAnnotations;

namespace YAPW.Models.DataModels;

/// <summary>
/// A generic Data model for adding a named entity
/// </summary>
public class AddViewDataModel
{
    /// <summary>
    /// The entity name
    /// </summary>
    [Display(Name = "Name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50)]
    public string Name { get; set; }


    [Display(Name = "TotalViews")]
    [Required(ErrorMessage = "TotalViews is required")]
    public int TotalViews { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public Guid TypeId { get; set; }

    /// <summary>
    /// The entity description
    /// </summary>
    [Display(Name = "Description")]
    [Required(ErrorMessage = "Description is required")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
}
