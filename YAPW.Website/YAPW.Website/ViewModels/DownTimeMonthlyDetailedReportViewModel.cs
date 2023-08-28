using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class DownTimeMonthlyDetailedReportViewModel
    {
        [Display(Name =("Line report"))]
        public LineReportViewModel LineReport { get; set; }

        [Display(Name =("byReason"))]
        public List<ReportByReasonViewModel> ByReason { get; set; }

        [Display(Name =("byStation"))]
        public List<ReportByStationViewModel> ByStation { get; set; }

        [Display(Name =("byDay"))]
        [JsonProperty("byDay")]
        public List<DailyReportViewModel> DailyReport { get; set; }
    }
}
