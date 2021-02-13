using System;
using System.Collections.Generic;
using System.Text;
using Yms.Contracts.Production;

namespace Yms.Services.OrderManagement.Abstractions
{
    public interface ICartService
    {
        bool Add(Guid userId, Guid productId, byte count);
        IEnumerable<CartDto> GetProductByUserId(Guid id);
    }
}
