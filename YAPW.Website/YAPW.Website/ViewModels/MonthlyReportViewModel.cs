using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class MonthlyReportViewModel
    {
        public string ZoneName { get; set; }

        public List<GroupedMonthlyViewModel> MonthlyReport { get; set; }
    }
}
