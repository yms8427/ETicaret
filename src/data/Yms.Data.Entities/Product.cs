using System;

namespace Yms.Data.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
