using System;
using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class HourlyOperationDetailsViewModel
    {
        public int TimeKey { get; set; }
        public int Count { get; set; }
        public List<HourlyOperationJobDetailsViewModel> Jobs { get; set; }

    }
    public class HourlyOperationJobDetailsViewModel
    {
        public DateTime TimeStamp { get; set; }
        public string Erp { get; set; }
        public string Serial { get; set; }
        public string CurrentStatus { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Order { get; set; }
    }
}
