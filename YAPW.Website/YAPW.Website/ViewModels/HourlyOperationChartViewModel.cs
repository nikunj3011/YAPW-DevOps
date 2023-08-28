using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class HourlyOperationChartViewModel
    {
        public string Name { get; set; }

        public List<int> Count { get; set; }

    }

    public class HourlyOperationFloorChartViewModel
    {
        public string name { get; set; }

        public List<int> data { get; set; }

    }
}
