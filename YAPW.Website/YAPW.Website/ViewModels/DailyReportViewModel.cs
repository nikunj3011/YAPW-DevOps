using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.ViewModels
{
    public class DailyReportViewModel
    {
        [JsonProperty("zoneName")]
        public string ZoneName { get; set; }

        [JsonProperty("dailyReports")]
        public List<GroupedDailyViewModel> DailyReports { get; set; }
    }
}
