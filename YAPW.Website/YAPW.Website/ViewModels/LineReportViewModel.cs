using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class LineReportViewModel
    {
        [Display(Name = ("Expected run duration"))]
        public string ExpectedRunDuration { get; set; }

        [Display(Name = ("Acual run duration"))]
        public string AcualRunDuration { get; set; }

        [Display(Name = ("Total Stops"))]
        public int TotalStops { get; set; }

        [Display(Name = ("Breaks"))]
        public string Breaks { get; set; }

        [Display(Name = ("Expected breaks"))]
        public string ExpectedBreaks { get; set; }

        [Display(Name = ("Down Time"))]
        public string DownTime { get; set; }

        [Display(Name = ("Down time with over breaks"))]
        public string DownTimeWithOverBreaks { get; set; }

        [Display(Name = ("Over breaks"))]
        public string OverBreaks { get; set; }

        [Display(Name = ("Down time percentage"))]
        public string DownTimePercentage { get; set; }
    }
}
