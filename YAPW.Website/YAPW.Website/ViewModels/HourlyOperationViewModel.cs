using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class HourlyOperationViewModel
    {
        public string Operation { get; set; }
        public int count { get; set; }
        public List<HourlyOperationDetailesViewModel> Hourly { get; set; }

    }
}
