using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yms.Data.Entities
{
    [Table("SubCategories", Schema = "Production")]
    public class SubCategory
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(140)]
        public Guid CategoryId { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
