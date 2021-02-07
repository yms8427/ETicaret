using System;
using System.Collections.Generic;
using System.Text;

namespace Yms.Services.OrderManagement.Abstractions
{
    public interface ICartService
    {
        bool Add(Guid userId, Guid productId, byte count);
    }
}
