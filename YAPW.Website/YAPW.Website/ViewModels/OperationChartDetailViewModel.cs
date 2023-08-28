using System;
using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class OperationChartDetailedViewModel
    {
        public string Erp { get; set; }
        public string Serial { get; set; }
        public int Size { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string CurrentStatus { get; set; }
        public string Order { get; set; }
        public string TankId { get; set; }
    }
}

