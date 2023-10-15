using System;
using YAPW.Models.DataModels;

namespace YAPW.Models
{
    public class VideoGetModel
    {
        public string? Search { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Order Order { get; set; }
        public int Page { get; set; }
    }

    public enum Order
    {
        Descending, Ascending/*, MostViews, LeastViews*/
    }
}