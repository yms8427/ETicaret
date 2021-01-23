using System;

namespace Yms.Contracts.Production
{
    public class NewProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
