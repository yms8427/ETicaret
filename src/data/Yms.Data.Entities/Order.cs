using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Orders", Schema = "Order")]
    public class Order : EntityBase
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public decimal Total { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}