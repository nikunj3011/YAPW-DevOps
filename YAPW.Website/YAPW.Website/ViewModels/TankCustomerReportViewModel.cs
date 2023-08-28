using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ditech.Portal.NET.ViewModels
{
    public partial class TankCustomerReportViewModel
    {
        [Display(Name = "Serial")]
        public string Serial { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Status")]
        public string TankStatus { get; set; }

        [Display(Name = "Pickup Order")]
        public string PickupOrder { get; set; }

        [Display(Name = "Pickup Adress")]
        public string PickupAdress { get; set; }

        [Display(Name = "Prickup Date")]
        public DateTime PickupDate { get; set; }

        [Display(Name = "Unload Date")]
        public DateTime UnloadDate { get; set; }

        [Display(Name = "Referbish Date")]
        public DateTime ReferbishDate { get; set; }

        [Display(Name = "Delivery Order")]
        public string DeliveryOrder { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Delivery Adress")]
        public string DeliveryAdress { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Display(Name = "Manufacturing Date")]
        public DateTime? ManufacturingDate { get; set; }
    }

    public partial class TankCustomerReportViewModel
    {
        public string PickupDateShort { 
            get
            {
                return PickupDate.ToShortDateString();
            }
        }
        public string UnloadDateShort {
            get
            {
                return UnloadDate.ToShortDateString();
            }
        }
        public string ReferbishDateShort {
            get
            {
                return ReferbishDate.ToShortDateString();
            }
        }

        public string DeliveryDateShort
        {
            get
            {
                return DeliveryDate.ToShortDateString();
            }
        }
        public string ManufacturingDateShort {
            get
            {
                return ManufacturingDate?.ToShortDateString();
            }
        }

    }

}
