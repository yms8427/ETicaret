using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    public class SupplierService : ISupplierService
    {
        private readonly YmsDbContext context;

        public SupplierService(YmsDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SupplierDto> GetSuppliers()
        {
            return context.Suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                Address = s.Address,
                Mail = s.Mail,
                Name = s.Name,
                Phone = s.Phone,
                TaxNumber = s.TaxNumber,
                Vote = s.Vote,
                VoteCount = s.VoteCount
            }).ToList();
        }
    }
}
