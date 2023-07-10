using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models.DataModels;

namespace YAPW.Domain.Repositories.Main;

public class ActorRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>, IActorService
    where TEntity : MainDb.DbModels.Actor
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public ActorRepository(TContext context) : base(context)
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

    public async Task<Actor> AddActor(string name)
    {
        //await SantitizeNamedEntityDataModel(ModelState, namedEntityDataModel, n => n.Name.ToLower() == namedEntityDataModel.Name.ToLower());
        var cc = new Actor
        {
            Name = name,
            Description = "",
            ProfilePhotoLink = "",
            TotalVideos = 100.ToString(),
        };
        await _serviceWorker.ActorRepository.AddAsync(cc);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return cc;
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