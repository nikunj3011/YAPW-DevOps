using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAPW.Models;

namespace YAPW.Domain.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDataModel>> GetLimited(int take);
        Task<IEnumerable<VideoDataModel>> SearchVideos(string name);
    }
}
