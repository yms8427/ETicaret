using System;

namespace Yms.Contracts.Production
{
    public class NewSubCategoryDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string ImgUrl { get; set; }
    }
}