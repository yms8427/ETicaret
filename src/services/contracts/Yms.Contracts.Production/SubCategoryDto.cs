using System;

namespace Yms.Contracts.Production
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImgUrl { get; set; }
    }
}