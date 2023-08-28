using System;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class UnloadOrderStatsViewModel
    {
        [Display(Name = "Order")]
        public long Order { get; set; }
        [Display(Name = "Customer")]
        public string Customer { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Jobs")]
        public int JobsCount { get; set; }
        [Display(Name = "Production Order")]
        public bool ProductionOrderCreated { get; set; }
        [Display(Name = "Picked up")]
        public int PickupCount { get; set; }
        [Display(Name = "Recorded")]
        public int UnloadCount { get; set; }
    }
}
