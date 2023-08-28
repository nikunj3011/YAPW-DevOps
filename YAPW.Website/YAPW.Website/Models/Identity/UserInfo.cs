using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.Models.Identity;
public class UserInfo
{
    [Display(Name = "User")]
    [Required]
    //[ForeignKey("Users")]
    public Guid UserID { get; set; }

    //public virtual User User { get; set; }

    [Display(Name = "First Name")]
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Middle Name")]
    [StringLength(50)]
    public string MiddleName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    [Required]
    public DateTime DateOfBirth { get; set; }

    //public virtual ICollection<UserInfoAddress> Addresses { get; set; }

    //[NotMapped]
    [JsonProperty("fullName")]
    public string FullName { get; set; }
}
