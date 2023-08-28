using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ditech.Api.Web.ViewModels.Legacy.Operation
{ 
    public class OperationsViewModel
    { 
        [JsonProperty("OperationName")]
        public string OperationName { get; set; } 
        [JsonProperty("OperationQuantity")]
        public int OperationQuantity { get; set; }
    }

    public class OperationsViewModel2
    { 
        [JsonProperty("OperationName")]
        public string OperationName { get; set; } 
        [JsonProperty("OperationQuantity")]
        public int OperationQuantity { get; set; } 
        public IEnumerable<OperationsDetailss> OperationsDetail { get; set; }
    }

    public class OperationsDetailss
    { 
        [JsonProperty("Job")]
        public string Job { get; set; } 
        [JsonProperty("Stock Code")]
        public string StockCode { get; set; } 
        [JsonProperty("TimeStamp")]
        public DateTime TimeStamp { get; set; } 
        [JsonProperty("Employee Number")]
        public decimal? EmployeeNumber { get; set; } 
        [JsonProperty("Customer")]
        public string Customer { get; set; } 
        [JsonProperty("Warehouse")]
        public string Warehouse { get; set; }
    }



}
