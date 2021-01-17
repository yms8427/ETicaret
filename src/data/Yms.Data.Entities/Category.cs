using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Categories", Schema = "Production")]
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(140)]
        public string Description { get; set; }
    }
}
