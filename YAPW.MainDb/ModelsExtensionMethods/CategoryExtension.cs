
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
    public static class CategoryExtension
    {
        public static CategoryDataModel AsCategoryDataModel(this Category category)
        {
            return new CategoryDataModel
            {
                Id = category.Id,
                Name = category.Name,
                PhotoLink = category.PhotoUrl.LinkId,
                Count = category.Count,
                Description = category.Description
            };
        }
    }
}
