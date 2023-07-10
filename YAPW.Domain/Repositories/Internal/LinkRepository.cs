using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb.DbModels;

namespace YAPW.Domain.Repositories.Main;

public class LinkRepository<TEntity, TContext> : EntityRepository<TEntity, TContext>, ILinkService
        where TEntity : Link, new()
        where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public LinkRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(context);
    }

    #region Get

    public async Task<Link> GetByLink(string link)
    {
        Guard.Against.NullOrWhiteSpace(link, nameof(link));

        //var type = await _serviceWorker.TypeRepository.FindSingleAsync(t => t.Id == typeId, false);
        //ArgumentNullException.ThrowIfNull(type, nameof(type));

        //var customer = await _serviceWorker.CustomerRepository.FindSingleByIDAsync(customerId, null);
        //ArgumentNullException.ThrowIfNull(customer, nameof(customer));
        //var cc = new TEntity();
        //cc.Id = Guid.NewGuid();
        //cc.Active = true;
        //cc.CreatedDate = DateTime.Now;
        //cc.LinkId = "2";
        //cc.LastModificationDate = DateTime.Now;
        //await AddAsync(cc);
        //await _context.SaveChangesAsync();
        return await FindSingleAsync(b => b.LinkId == link, null);
    }

    public Task<IEnumerable<dynamic>> SearchTypes(string name, int take)
    {
        throw new NotImplementedException();
    }
    #endregion Get

    #region Helpers

    //private async Task<Category> GetVehicleTypeCategory()
    //{
    //    var vehicleTypeCategory = await _serviceWorker.CategoryRepository.FindSingleByNameAsync("vehicle type", false);
    //    Guard.Against.Null(vehicleTypeCategory, nameof(vehicleTypeCategory));
    //    return vehicleTypeCategory;
    //}

    #endregion Helpers
}