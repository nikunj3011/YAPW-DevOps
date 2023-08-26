
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
    public static class BrandExtension
    {
        public static BrandDataModel AsBrandDataModel(this Brand brand)
        {
            return new BrandDataModel
            {
                Id = brand.Id,
                Name = brand.Name,
                PhotoLink = brand.Logo.LinkId,
                Count = brand.Count,
                Description = brand.Description
            };
        }
    }
}
