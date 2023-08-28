using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class ShippedTankQuarterlyReportViewModel
    {
        public string Unit { get; set; }
        public List<TankSizeMonthlyShip> Sizes { get; set; }
    }
    public class TankSizeQuarterlyShip
    {
        public string Size { get; set; }
        public int Qty { get; set; }
    }
}
