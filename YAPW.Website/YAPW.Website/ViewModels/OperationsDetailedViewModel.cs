using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.ViewModels
{ 
    public class OperationsDetailedViewModel
    { 
        public int Hour { get; set; } 
        public int OperationQuantity { get; set; }
    }
    public class OperationsDetailedViewModel2
    {
        public int Hour { get; set; }
        public int OperationQuantity { get; set; }
        public IEnumerable<OperationsDetails> OperationsDetail { get; set; }
    }
    public class OperationsDetails{ 

        [Display(Name ="Job")]
        [JsonProperty("job")] 
        public string Job { get; set; }

        [Display(Name = "Stock Code")]
        [JsonProperty("Stock Code")] 
        public string StockCode { get; set; }

        [Display(Name = "Time stamp")]
        [JsonProperty("timestamp")] 
        public string TimeStamp { get; set; }

        [Display(Name = "Employee Id")]
        [JsonProperty("Employee Number")] 
        public decimal? EmployeeNumber { get; set; }

        [Display(Name = "Customer")]
        [JsonProperty("customer")] 
        public string Customer { get; set; }

        [Display(Name = "Warehouse")]
        [JsonProperty("warehouse")] 
        public string Warehouse { get; set; }
    } 
}
