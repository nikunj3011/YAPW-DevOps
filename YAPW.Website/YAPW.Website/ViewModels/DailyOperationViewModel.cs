using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class DailyOperationViewModel
    {
        public string name { get; set; }
        public int count { get; set; }
        public List<OperationChartDetailedViewModel> jobs { get; set; }
    }
}
