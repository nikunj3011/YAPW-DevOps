using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;
using YAPW.MainDb.Interfaces;
using YAPW.Models;

namespace YAPW.Domain.Repositories.Main;

public class VideoRepository<TEntity, TContext> : NamedEntityRepository<TEntity, TContext>, IVideoService
    where TEntity : MainDb.DbModels.Video
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ServiceWorker<TContext> _serviceWorker;
    private readonly Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> _videoIncludes;

    public VideoRepository(TContext context) : base(context)
    {
        _context = context;
        _serviceWorker = new ServiceWorker<TContext>(_context);
        _videoIncludes = GetIncludes();
    }

    public async Task<IEnumerable<VideoDataModel>> GetLimited(int take)
    {
        var videos = await FindAsyncNoSelect(take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<IEnumerable<VideoDataModel>> GetLimitedByReleaseDate(int take)
    {
        var videos = await FindAsyncNoSelect(orderBy:p=>p.OrderByDescending(p=>p.VideoInfo.ReleaseDate) ,take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<VideoSearchResponse> SearchWithPagination(VideoGetModel videoGetModel)
    {
        IEnumerable<TEntity> videos = new List<TEntity>();
        if(videoGetModel.Order == Order.Ascending)
        {
            videos = await FindAsyncNoSelect(filter: p => p.Name.ToLower().Contains(videoGetModel.Search) && p.BrandId == videoGetModel.BrandId /*&& p.VideoCategories.Select(l => l.Id).Contains(videoGetModel.CategoryId)*/, orderBy: p => p.OrderBy(p => p.VideoInfo.ReleaseDate), include: _videoIncludes).ConfigureAwait(false);
        }
        else if (videoGetModel.Order == Order.Descending)
        {
            videos = await FindAsyncNoSelect(filter: p => p.Name == videoGetModel.Search && p.BrandId == videoGetModel.BrandId /*&& p.VideoCategories.Select(l => l.Id).Contains(videoGetModel.CategoryId)*/, orderBy: p => p.OrderByDescending(p => p.VideoInfo.ReleaseDate), include: _videoIncludes).ConfigureAwait(false);
        }
        var pageResult = 20f;
        var pageCount = Math.Ceiling(videos.Count() / pageResult);
        videos = videos.Skip((videoGetModel.Page) * (int)pageResult).Take((int)pageResult).ToList();

        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return new VideoSearchResponse
        {
            Videos = videosOp,
            CurrentPage = videoGetModel.Page,
            Pages = (int)pageCount,
        };
    }

    public async Task<IEnumerable<VideoDataModel>> GetLimitedByViews(int take)
    {
        var videos = await FindAsyncNoSelect(orderBy: p => p.OrderByDescending(p => p.VideoInfo.Views).ThenByDescending(p=>p.VideoInfo.ReleaseDate), take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<IEnumerable<VideoDataModel>> GetRandomLimited(int take)
    {
        var videos = await FindRandomAsyncNoSelect(take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<IEnumerable<VideoDataModel>> GetRandomLimitedByBrand(string brandName, int take)
    {
        var videos = await FindRandomAsyncNoSelect(filter: p=>p.Brand.Name.ToLower() == brandName.ToLower(),take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<VideoDataModel> GetByNameDetailed(string name)
    {
        try
        {
            var video = await FindSingleAsync(t => t.Name.ToLower() == name.ToLower(), _videoIncludes).ConfigureAwait(false);
            if(video != null)
            {
                return video.AsVideoDataModel();
            }
            throw new Exception("No video found");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<VideoDataModel>> SearchTypes(string name, int take)
    {
        return await FindAsync(filter: v => v.Name.ToLower().Contains(name.ToLower()), take: take, select: t => new VideoDataModel
        {
            Id = t.Id,
            Name = t.Name,
        }, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    }

    //public async Task<IEnumerable<VideoDataModel>> AddVideo(string name, int take)
    //{
    //	return await FindAsync(filter: v => v.Name.ToLower().Contains(name.ToLower()), take: take, select: t => new
    //	{
    //		t.Id,
    //		t.Name
    //	}, orderBy: t => t.OrderBy(t => t.Name)).ConfigureAwait(false);
    //}
    #region Helpers
    public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> GetIncludes()
    {
        return p => p
                .Include(p => p.Brand)
                .Include(p => p.VideoCategories).ThenInclude(p => p.Category)
                .Include(p => p.VideoInfo).ThenInclude(p => p.VideoUrl)
                .Include(p => p.VideoInfo).ThenInclude(p => p.Cover)
                .Include(p => p.VideoInfo).ThenInclude(p => p.Poster)
                .Include(p => p.VideoInfo).ThenInclude(p => p.VideoTitles);
    }
    #endregion Helpers
}