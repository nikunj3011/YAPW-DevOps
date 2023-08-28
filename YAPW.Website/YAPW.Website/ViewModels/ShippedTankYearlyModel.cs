using System.Collections.Generic;
using System.Drawing;

namespace Ditech.Portal.NET.ViewModels
{
    public class ShippedTankYearlyModel
    {
        public int year { get; set; }
        public int qty { get; set; }
        public List<Size> sizes { get; set; }
    }
    public class Size
    {
        public string size { get; set; }
        public int qty { get; set; }
    }
}
