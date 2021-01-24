using System;

namespace Yms.Contracts.Production
{
    public class DetailedSubCategoryDto
    {
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
    }
}