using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models;

namespace YAPW.Domain.Repositories.Main;

public class BrandRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>, IBrandService
    where TEntity : MainDb.DbModels.Brand
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;
    private readonly DbSet<TEntity> _dbSet;

    public BrandRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(_context);
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<dynamic>> SearchTypes(string name, int take)
    {
        return await FindAsync(filter: v => v.Name.ToLower().Contains(name.ToLower()), take: take, select: t => new
        {
            t.Id,
            t.Name
        }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    }

    public async Task<IEnumerable<dynamic>> GetAllMinimal()
    {
        return await FindAsync(select: t => new
        {
            t.Id,
            t.Name
        }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    }

    public async Task<IEnumerable<BrandDataModel>> GetRandomLimited(int take)
    {
        var brandsDb = await FindRandomAsyncNoSelect(take: take, include: p => p.Include(p => p.Logo)).ConfigureAwait(false);
        var brands = new List<BrandDataModel>();
        foreach (var item in brandsDb)
        {
            var videoImage = await _serviceWorker.VideoRepository.FindAsync(filter: v => v.Brand.Name.ToLower() == item.Name.ToLower(), take: take, select: t => new
            {
                t.Id,
                t.Name,
                t.VideoInfo.Cover.LinkId
            }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
            item.Logo.LinkId = videoImage?.FirstOrDefault().LinkId;
            brands.Add(item.AsBrandDataModel());
        }
        return brands;
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