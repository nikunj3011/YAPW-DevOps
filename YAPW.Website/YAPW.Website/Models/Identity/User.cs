using Ditech.Portal.NET.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.Models.Identity;
public class User
{
    [Required]
    [Display(Name = "Is Active")]
    public bool Active { get; set; }

    [Required]
    [Display(Name = "Creator")]
    public Guid CreatorID { get; set; }

    [Required]
    [Display(Name = "Last Modification Date")]
    [DataType(DataType.DateTime)]
    public DateTime LastModificationDate { get; set; }

    [Required]
    [Display(Name = "Created date")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Client Id is used to determine which client was responsible of adding the user
    /// </summary>
    [Required]
    [Display(Name = "Client")]
    public Guid ClientId { get; set; }

    public virtual UserInfo UserInfo { get; set; }

    public virtual ICollection<EntityField> ExtraFields { get; set; }
}
