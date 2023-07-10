//using Microsoft.EntityFrameworkCore;
//using YAPW.Domain.Interfaces;
//using YAPW.MainDb;

//namespace YAPW.Domain.Services.Internal
//{
//    public class TypeService : ITypeService
//    {
//        private readonly DataContext _context;

//        public TypeService(DataContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<MainDb.DbModels.Type>> GetAll()
//        {
//            return await _context.Types.ToListAsync();
//        }
//    }
//}
