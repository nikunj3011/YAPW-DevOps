using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;

namespace YAPW.Domain.Repositories.Main;

public class TypeRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>
    where TEntity : MainDb.DbModels.Type
    where TContext : DataContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public TypeRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(_context);
    }

    //public async Task<IEnumerable<TEntity>> FindTrucks()
    //{
    //    //return await FindAsync(v => v.TypeId == vehicleType.Id).ConfigureAwait(false);
    //}

    #region Helpers

    //private async Task<Category> GetVehicleTypeCategory()
    //{
    //    var vehicleTypeCategory = await _serviceWorker.CategoryRepository.FindSingleByNameAsync("vehicle type", false);
    //    Guard.Against.Null(vehicleTypeCategory, nameof(vehicleTypeCategory));
    //    return vehicleTypeCategory;
    //}

    #endregion Helpers
}