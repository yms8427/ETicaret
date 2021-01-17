using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Common.Contracts.Enums;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("ClientAddresses", Schema = "Management")]
    public class ClientAddress : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public AddressType Type { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
