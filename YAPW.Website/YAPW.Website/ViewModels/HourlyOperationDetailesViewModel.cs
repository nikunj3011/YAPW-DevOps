using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class HourlyOperationDetailesViewModel
    {
        public int TimeKey { get; set; }
        public int Count { get; set; }
        public List<OperationChartDetailedViewModel> Jobs { get; set; }
    }
}
