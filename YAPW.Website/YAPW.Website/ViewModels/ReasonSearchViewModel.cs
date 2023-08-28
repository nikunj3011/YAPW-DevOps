using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.ViewModels
{
    public class ReasonSearchViewModel
    {
        //[JsonProperty("")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type{ get; set; }
    }
}
