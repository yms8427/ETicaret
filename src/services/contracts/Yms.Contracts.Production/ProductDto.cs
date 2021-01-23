using System;

namespace Yms.Contracts.Production
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public Guid Id { get; set; }
    }
}
