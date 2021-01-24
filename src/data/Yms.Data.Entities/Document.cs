using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Documents", Schema = "Management")]
    public class Document : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string PhysicalAddress { get; set; }
    }
}
