using System;

namespace Yms.Data.Entity.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        Guid CreatedBy { get; set; }
        DateTime Updated { get; set; }
        Guid UpdatedBy { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
    }
}
