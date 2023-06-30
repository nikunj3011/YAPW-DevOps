using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.Models.DataModels;

namespace YAPW.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public abstract class GenericNamedEntitiesControllerBase<TEntity, TContext, TServiceWorker, TNamedEntityDataModel> : GenericEntitiesControllerBase<TEntity, TContext, TServiceWorker>
    where TEntity : NamedEntity, new()
    where TContext : DataContext
    where TServiceWorker : NamedEntityServiceWorker<TEntity, TContext>
    where TNamedEntityDataModel : NamedEntityDataModel
{
    private readonly TServiceWorker _entityServiceWorker;

    protected GenericNamedEntitiesControllerBase(
        TServiceWorker entityServiceWorker,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostingEnvironment) : base(
            entityServiceWorker,
            httpContextAccessor,
            hostingEnvironment)
    {
        _entityServiceWorker = entityServiceWorker;
    }

    //TO DO:
    ////Add a generic way to add custom fields
    //End TO DO
    /// <summary>
    /// Post Entity
    /// </summary>
    /// <param name="namedEntityDataModel"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post(TNamedEntityDataModel namedEntityDataModel)
    {
        try
        {
            await SantitizeNamedEntityDataModel(ModelState, namedEntityDataModel, n => n.Name.ToLower() == namedEntityDataModel.Name.ToLower());
            var entity = GenerateNamedEntity(namedEntityDataModel);
            await SaveEntityToDb(entity);
            return StatusCode(StatusCodes.Status201Created, entity);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateWaitObjectException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Get entity by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("ByName/{name}")]
    public virtual async Task<ActionResult<TEntity>> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Name is required");
        }
        var entityTypeName = typeof(TEntity).Name;
        var entity = await _entityServiceWorker.NamedEntityRepository.FindSingleByNameAsync(name);
        if (entity == null)
        {
            return NotFound($"Unable to find a {entityTypeName} named'{name}'.");
        }
        return entity;
    }

    #region Helpers

    protected async Task<TEntity> SaveEntityToDb(TEntity entity)
    {
        await _entityServiceWorker.NamedEntityRepository.AddAsync(entity);
        await _entityServiceWorker.SaveAsync().ConfigureAwait(false);
        return entity;
    }

    protected async Task SantitizeNamedEntityDataModel(ModelStateDictionary modelState, TNamedEntityDataModel namedEntityDataModel, Expression<Func<TEntity, bool>> filter, string errorMessage = "")
    {
        if (!modelState.IsValid)
        {
            throw new BadHttpRequestException(modelState.ToString());
        }
        var entityTypeName = typeof(TEntity).Name;

        if (await _entityServiceWorker.NamedEntityRepository.ExistsAsync(filter))
        {
            var error = string.IsNullOrWhiteSpace(errorMessage) ? $"A {entityTypeName} named '{namedEntityDataModel.Name}' exists already." : errorMessage;
            throw new DuplicateWaitObjectException(error);
        }
    }

    protected TEntity GenerateNamedEntity(TNamedEntityDataModel namedEntityDataModel)
    {
        return new TEntity
        {
            Name = namedEntityDataModel.Name,
            Description = namedEntityDataModel.Description,
        };
    }

    #endregion Helpers
}
