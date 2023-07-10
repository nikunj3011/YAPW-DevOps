using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Interfaces
{
    public interface ILinkService
    {
        Task<Link> GetByLink(string link);
    }
}
