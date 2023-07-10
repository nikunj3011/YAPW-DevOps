using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;

namespace YAPW.Domain.Repositories.Main;

public class VideoRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>, IVideoService
    where TEntity : MainDb.DbModels.Video
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public VideoRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(_context);
    }

    public async Task<IEnumerable<dynamic>> SearchTypes(string name, int take)
    {
        return await FindAsync(filter: v => v.Name.ToLower().Contains(name.ToLower()), take: take, select: t => new
        {
            t.Id,
            t.Name
        }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    }

    #region Helpers

    //private async Task<Category> GetVehicleTypeCategory()
    //{
    //    var vehicleTypeCategory = await _serviceWorker.CategoryRepository.FindSingleByNameAsync("vehicle type", false);
    //    Guard.Against.Null(vehicleTypeCategory, nameof(vehicleTypeCategory));
    //    return vehicleTypeCategory;
    //}

    #endregion Helpers
}