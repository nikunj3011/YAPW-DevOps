using Ditech.Portal.NET.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ditech.Portal.NET.Models.Shared
{
    public class Address : EntityBase
    {
        [Display(Name = "Address")]
        [Required]
        public string AddressLine { get; set; }

        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Province or State")]
        public string ProvinceOrState { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

      
        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }
    }
}
