using System;
using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class TanksBySizePerDayViewModel
    {
        public DateTime Date { get; set; }
        public List<CustomerDashboardTanksBySize> Sizes { get; set; }
    }
}
