using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models;

namespace YAPW.Domain.Repositories.Main;

public class CategoryRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>, ICategoryService
    where TEntity : MainDb.DbModels.Category
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public CategoryRepository(TContext context) : base(context)
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

    public async Task<IEnumerable<CategoryDataModel>> GetAllMinimal()
    {
        return await FindAsync(select: t => new CategoryDataModel
        {
            Id = t.Id,
            Name = t.Name,
            PhotoLink = t.PhotoUrl.LinkId,
            Count = t.Count,
            Description = t.Description
        }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    }

    public async Task<IEnumerable<CategoryDataModel>> GetRandomLimited(int take)
    {
        var categoriesDb = await FindRandomAsyncNoSelect(take: take, include: p => p.Include(p=>p.PhotoUrl)).ConfigureAwait(false);
        var categories = new List<CategoryDataModel>();
        foreach (var item in categoriesDb)
        {
            categories.Add(item.AsCategoryDataModel());
        }
        return categories;
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