using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.Domain.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<dynamic>> SearchTypes(string name, int take);
    }
}
