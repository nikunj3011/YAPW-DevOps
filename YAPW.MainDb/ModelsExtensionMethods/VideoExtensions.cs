
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAPW.MainDb.DbModels;
using YAPW.Models;

namespace YAPW.MainDb.DbModels
{
    // Extension method
    public static class VideoExtension
    {
        public static VideoDataModel AsVideoDataModel(this Video video)
        {
            var videoCategories = new List<CategoryDataModel>();
            foreach (var item in video?.VideoCategories)
            {
                var videoCategory = new VideoCategoryDataModel();
                videoCategory.Category = new CategoryDataModel
                {
                    Id = item.Id,
                    Name = item.Category.Name,
                    Count = item.Category.Count,
                    Description = item.Category.Description
                };
                videoCategories.Add(videoCategory.Category);
            }
            var videoTitles = new List<string>();
            foreach (var videoTitle in video.VideoInfo.VideoTitles)
            {
                videoTitles.Add(videoTitle.Name);
            }
            return new VideoDataModel
            {
                Id = video.Id,
                Name = video.Name,
                BrandId = video.BrandId,
                Categories = videoCategories,
                Description = video.Description,
                CreatedDate = video.CreatedDate,
                LastModificationDate = video.LastModificationDate,
                Brand = new BrandDataModel
                {
                    Name = video.Brand.Name,
                    Description = video.Brand.Description,
                },
                VideoInfo = new VideoInfoDataModel
                {
                    Likes = video.VideoInfo.Likes,
                    Dislikes = video.VideoInfo.Dislikes,
                    Views = video.VideoInfo.Views,
                    ReleaseDate = video.VideoInfo.ReleaseDate,
                    VideoLength = video.VideoInfo.VideoLength,
                    IsCensored = video.VideoInfo.IsCensored,
                    VideoUrl = video.VideoInfo.VideoUrl.LinkId,
                    Cover = video.VideoInfo.Cover.LinkId,
                    Poster = video.VideoInfo.Poster.LinkId,
                    VideoTitles = videoTitles
                }
            };
        }
    }
}
