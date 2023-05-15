using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Pornstar : NamedEntity
    {
        public string ProfilePhotoLink { get; set; }

        public string TotalVideos { get; set; }
        //public string TotalVideos { get; set; }
    }
}
