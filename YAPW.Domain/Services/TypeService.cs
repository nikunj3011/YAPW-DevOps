using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAPW.Domain.Interfaces;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Services
{
    public class TypeService : ITypeService
    {
        private readonly DataContext _context;

        public TypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MainDb.DbModels.Type>> GetAll()
        {
            return await _context.Types.ToListAsync();
        }
    }
}
