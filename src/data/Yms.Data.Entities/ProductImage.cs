using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("ProductImages", Schema = "Production")]
    public class ProductImage : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
