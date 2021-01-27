﻿using System.Collections.Generic;
using Yms.Contracts.Production;

namespace Yms.Services.Production.Abstractions
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetSuppliers();
    }
}
