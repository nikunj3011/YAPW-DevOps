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

	public async Task UpdateView(string name, int count)
    {
        try
        {
            var viewPage = await _serviceWorker.ViewRepository.FindSingleAsync(p => p.Name == name, null);
            viewPage.TotalViews = viewPage.TotalViews + count;
            _serviceWorker.ViewRepository.Update(viewPage);
            await _serviceWorker.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}