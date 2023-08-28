using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class ReportByStationViewModel
    {
        [Display(Name = ("Station"))]
        public string Station { get; set; }

        [Display(Name = ("Duration"))]
        public string Duration { get; set; }

        [Display(Name = ("Occurrence"))]
        public int Occurrence { get; set; }

        [Display(Name = ("Reasons"))]
        public string Reasons { get; set; }
    }
}
