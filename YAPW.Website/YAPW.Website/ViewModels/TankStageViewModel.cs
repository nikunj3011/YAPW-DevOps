using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public partial class TankStageViewModel
    {
        [Display(Name = "Order")]
        public string Order { get; set; }
        
        [Display(Name = "OrderId")]
        public string OrderId { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }
        
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Production Start Date")]
        public DateTime ProductionStartDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        
        [Display(Name = "Tank Operation")]
        public List<Tanks> Tanks { get; set; }
    }

    public partial class TankStageViewModel
    {
        public string ProductionDateFormated
        {
            get
            {
                return ProductionStartDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public string CreatedDateFormated
        {
            get
            {
                return ProductionStartDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

    }

    public class TankSize
    {
        public int Size { get; set; }
        public int Count { get; set; }
    }

    public class Tanks
    {
        [Display(Name = "Status")]
        public string Status { get; set; }
        
        [Display(Name = "Count")]
        public int Count { get; set; }
        
        [Display(Name = "OperationId")]
        public string Operation { get; set; }

        public List<TankSize> Sizes { get; set; }
    }
}
