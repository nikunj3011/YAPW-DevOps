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
        var videos = await FindAsyncNoSelect(take: take, include: _videoIncludes, orderBy: p=>p.OrderByDescending(p=>p.VideoInfo.CreatedDate)).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos.Item1)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<IEnumerable<VideoDataModel>> GetLimitedByReleaseDate(int take)
    {
        var videos = await FindAsyncNoSelect(orderBy:p=>p.OrderByDescending(p=>p.VideoInfo.ReleaseDate) ,take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos.Item1)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<VideoSearchResponse> SearchWithPagination(VideoGetModel videoGetModel)
    {
        (IEnumerable<TEntity>, int) videos = (new List<TEntity>(), 0);

        decimal pageResult = 20;
        var pageCountAndSkip = FindPageCountAndSkip(pageResult, videoGetModel.Page);
        if (videoGetModel.Order == Order.Ascending)
        {
            videos = await FindAsyncNoSelect(take: (int)pageResult, skip: pageCountAndSkip.skip, filter: p => p.Name.ToLower().Contains(videoGetModel.Search)
                                                                                                 && p.BrandId.ToString().Contains(videoGetModel.BrandId == null ? "" : videoGetModel.BrandId.ToString())
                                                                                                 && p.VideoCategories.Any(l => l.CategoryId.ToString().Contains(videoGetModel.CategoryId == null ? "" : videoGetModel.CategoryId.ToString())), orderBy: p => p.OrderBy(p => p.VideoInfo.ReleaseDate), include: _videoIncludes).ConfigureAwait(false);
        }
        else if (videoGetModel.Order == Order.Descending)
        {
            videos = await FindAsyncNoSelect(take: (int)pageResult, skip: pageCountAndSkip.skip, filter: p => p.Name.ToLower().Contains(videoGetModel.Search)
                                                                                                 && p.BrandId.ToString().Contains(videoGetModel.BrandId == null ? "" : videoGetModel.BrandId.ToString())
                                                                                                 && p.VideoCategories.Any(l => l.CategoryId.ToString().Contains(videoGetModel.CategoryId == null ? "" : videoGetModel.CategoryId.ToString())), orderBy: p => p.OrderByDescending(p => p.VideoInfo.ReleaseDate), include: _videoIncludes).ConfigureAwait(false);
        }
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos.Item1)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        var responce =  new VideoSearchResponse
        {
            Videos = videosOp,
            CurrentPage = videoGetModel.Page,
            Pages = (int)videos.Item2/(int)pageResult,
        };
        return responce;
        (decimal pageCount, int skip) FindPageCountAndSkip(decimal pageResult, int page)
        {
            var pageCount = Math.Ceiling(FindCount() / pageResult);
            var skip = (videoGetModel.Page) * (int)pageResult;
            return (pageCount, skip);
        }
    }

    public async Task<IEnumerable<VideoDataModel>> GetLimitedByViews(int take)
    {
        var videos = await FindAsyncNoSelect(orderBy: p => p.OrderByDescending(p => p.VideoInfo.Views).ThenByDescending(p=>p.VideoInfo.ReleaseDate), take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos.Item1)
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
        var videos = await FindRandomAsyncNoSelect(filter: p=> p.Brand.Name.ToLower() == brandName.ToLower(), take: take, include: _videoIncludes).ConfigureAwait(false);
        var videosOp = new List<VideoDataModel>();
        foreach (var item in videos)
        {
            videosOp.Add(item.AsVideoDataModel());
        }
        return videosOp;
    }

    public async Task<VideoDataModel> GetByNameDetailed(string linkName)
    {
        try
        {
            var video = await FindSingleAsync(t => t.VideoInfo.VideoUrl.LinkId.ToLower() == linkName.ToLower(), _videoIncludes).ConfigureAwait(false);
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

    public async Task AddVideo(AddVideoDataModel item)
    {
        try
        {
            var videoTitles = new List<VideoTitle>();
            foreach (var videoTitle in item.Titles)
            {
                var newVideoTitle = new VideoTitle
                {
                    Name = videoTitle,
                    Description = ""
                };
                videoTitles.Add(newVideoTitle);
                _serviceWorker.VideoTitleRepository.Add(newVideoTitle);
            }

            var posterLink = new Link
            {
                LinkId = item.PosterUrl
            };
            _serviceWorker.LinkRepository.Add(posterLink);

            var coverLink = new Link
            {
                LinkId = item.CoverUrl
            };
            _serviceWorker.LinkRepository.Add(coverLink);

            var videoLink = new Link
            {
                LinkId = item.Slug
            };
            _serviceWorker.LinkRepository.Add(videoLink);

            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime createdDate = start.AddSeconds(item.CreatedAt).ToLocalTime();

            long realeaseDate = item.ReleasedAt;
            start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime releaseDate = start.AddSeconds(item.ReleasedAt).ToLocalTime();
            var brand = await _serviceWorker.BrandRepository.FindSingleAsync(p => string.Equals(p.Name, item.Brand, StringComparison.OrdinalIgnoreCase), null);
            ArgumentNullException.ThrowIfNull(brand);
            var newVideo = new Video
            {
                Name = item.Name,
                Description = item.Description,
                BrandId = brand.Id,
                VideoCategories = new List<VideoCategory>(),
                //Link = videoLink,
                //Photo = null
                //VideoInfo = new VideoInfo(),
            };
            _serviceWorker.VideoRepository.Add(newVideo);

            var newVideoInfo = new VideoInfo
            {
                Views = 0,
                VideoTitles = videoTitles,
                VideoUrlId = videoLink.Id,
                PosterId = posterLink.Id,
                CoverId = coverLink.Id,
                VideoLength = 0,
                IsCensored = item.IsCensored,
                CreatedDate = createdDate,
                ReleaseDate = releaseDate,
                VideoId = newVideo.Id
            };
            _serviceWorker.VideoInfoRepository.Add(newVideoInfo);

            var videoCateogries = new List<VideoCategory>();
            foreach (var tag in item.Tags)
            {
                var category = await _serviceWorker.CategoryRepository.FindSingleAsync(p => string.Equals(p.Name, tag, StringComparison.OrdinalIgnoreCase), null);
                ArgumentNullException.ThrowIfNull(category);
                var newVideoCategory = new VideoCategory
                {
                    CategoryId = category.Id,
                    //Category = category,
                    VideoId = newVideo.Id
                };
                _serviceWorker.VideoCategoryRepository.Add(newVideoCategory);
                await _serviceWorker.SaveAsync();

                videoCateogries.Add(newVideoCategory);
            }
            newVideo.VideoCategories = videoCateogries;
            newVideo.VideoInfo = newVideoInfo;

            _serviceWorker.VideoRepository.Update(newVideo);
            await _serviceWorker.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

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