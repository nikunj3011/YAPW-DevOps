using Newtonsoft.Json;
using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class AddVideoDataModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("titles")]
        public string[] Titles { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("poster_url")]
        public string PosterUrl { get; set; }

        [JsonProperty("cover_url")]
        public string CoverUrl { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("is_censored")]
        public bool IsCensored { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("released_at")]
        public long ReleasedAt { get; set; }
    }
}