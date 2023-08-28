using System.Collections.Generic;

namespace Ditech.Portal.NET.ViewModels
{
    public class DailyOperationRageViewModel
    {
        public string Date { get; set; }
        public List<Operation> Operations { get; set; }
    }
    public class Operation
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
