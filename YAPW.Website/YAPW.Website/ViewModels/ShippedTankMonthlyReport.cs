using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class ShippedTankMonthlyReport
    {
        public int Month { get; set; }
        public List<TankSizeMonthlyShip> Sizes { get; set; }
    }
    public class TankSizeMonthlyShip
    {
        public string Size { get; set; }
        public int Qty { get; set; }
    }
}
