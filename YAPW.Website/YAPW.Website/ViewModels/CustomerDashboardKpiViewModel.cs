using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class CustomerDashboardKpiViewModel
    {
        public int Received { get; set; }
        public int Shipped { get; set; }
        public int Scrapped { get; set; }

    }

    public class CustomerDashboardTanksBySize
    {
        public string tanksize { get; set; }
        public int count { get; set; }
    }

    public class CustomerDashboardTanksBySizePerLocationViewModel
    {
        public string tankCity { get; set; }
        public string customerShortName { get; set; }
        public List<CustomerDashboardTanksBySize> sizes { get; set; }
    }
    public class CustomerDashboardTanksByLocationViewModel
    {
        public string tankCity { get; set; }
        public string customerShortName { get; set; }
        public int count { get; set; }
    }

}
