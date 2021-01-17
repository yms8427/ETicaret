using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Invoices", Schema = "Order")]
    public class Invoice : EntityBase
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public int InvoiceNumber { get; set; }
        [Required]
        public Guid ClientAddressId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(ClientAddress))]
        public virtual ClientAddress ClientAddress { get; set; }
    }
}
