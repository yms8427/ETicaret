using System;
using System.ComponentModel.DataAnnotations;

namespace Yms.Data.Entity.Abstractions
{
    public class EntityBase : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
