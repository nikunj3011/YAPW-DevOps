using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Actor : NamedEntity
    {
        public string ProfilePhotoLink { get; set; }

        public int TotalVideos { get; set; }
        //public string TotalVideos { get; set; }
    }
}
