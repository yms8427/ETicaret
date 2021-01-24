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
        public Guid DocumentId { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(DocumentId))]
        public virtual Document Document { get; set; }
    }
}
