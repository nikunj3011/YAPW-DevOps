using Newtonsoft.Json;

namespace YAPW.Models
{
    public class NameDataModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("gender")]
        public bool Gender { get; set; }

        [JsonProperty("birthDay")]
        public DateTimeOffset BirthDay { get; set; }
    }
}