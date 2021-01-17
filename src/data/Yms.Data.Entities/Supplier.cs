using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Suppliers", Schema = "Production")]
    public class Supplier : EntityBase
    {
        [Required]
        [MaxLength(140)]
        public string Name { get; set; }

        [Required]
        [MaxLength(60)]
        public string Mail { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(12)]
        public string TaxNumber { get; set; }

        public int Vote { get; set; }

        public int VoteCount { get; set; }
    }
}