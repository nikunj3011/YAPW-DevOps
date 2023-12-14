using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class ViewDataModel : NamedEntityDataModel
    {
        [JsonProperty("totalViews")]
        public long TotalViews { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}