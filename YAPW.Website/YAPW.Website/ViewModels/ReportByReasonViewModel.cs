using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class ReportByReasonViewModel
    {
        [Display(Name = ("Reason"))]
        public string Reason { get; set; }

        [Display(Name = ("Duration"))]
        public string Duration { get; set; }

        [Display(Name = ("Occurrence"))]
        public int Occurrence { get; set; }
    }
}
