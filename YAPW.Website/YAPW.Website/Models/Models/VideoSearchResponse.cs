using System.Collections.Generic;

namespace YAPW.Models
{
    public class VideoSearchResponse
    {
        public List<VideoDataModel> Videos { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}