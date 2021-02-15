using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Products", Schema = "Production")]
    public class Product : EntityBase
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public Guid? DocumentId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Supplier Supplier { get; set; }
        public SubCategory SubCategory { get; set; }
        public Document Document { get; set; }
    }
}
