using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Common.Contracts.Enums;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("OrderTracks", Schema = "Order")]
    public class OrderTrack : EntityBase
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public OrderStatus Status { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
