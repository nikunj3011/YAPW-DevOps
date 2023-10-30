using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
using System.Linq;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.MainDb.Interfaces;
using YAPW.Models;
using YAPW.Models.DataModels;

namespace YAPW.Domain.Repositories.Main;

public class ViewRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>
    where TEntity : MainDb.DbModels.View
	where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;

    public ViewRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(_context);
    }

	public async Task<string> UpdateView(AddViewDataModel addViewDataModel)
    {
        try
        {
            var view = await _serviceWorker.ViewRepository.FindSingleAsync(p => p.Name == addViewDataModel.Name, null);
            if (view == null)
            {
                await _serviceWorker.ViewRepository.AddAsync(new View
                {
                    Name = addViewDataModel.Name,
                    Description = addViewDataModel.Description,
                    TotalViews = addViewDataModel.TotalViews,
                    TypeId = addViewDataModel.TypeId
                });
                await _serviceWorker.SaveAsync();
            }
            else
            {
                view.TotalViews += addViewDataModel.TotalViews;
                _serviceWorker.ViewRepository.Update(view);
                await _serviceWorker.SaveAsync();
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}