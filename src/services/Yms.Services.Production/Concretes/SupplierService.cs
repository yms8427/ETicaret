using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
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

        public string GetSupplierNameById(Guid id)
        {
            return context.Suppliers.FirstOrDefault(f => f.Id == id)?.Name;
        }




        public IEnumerable<SupplierDto> GetSuppliers()
        {
            return context.Suppliers.OrderByDescending(s => s.Created).Select(s => new SupplierDto
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

        public Guid AddNewSupplier(NewSupplierDto data)
        {
            var s = new Supplier
            {
                Address = data.Address,
                Created = DateTime.Now,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Id = Guid.NewGuid(),
                IsActive = true,
                IsDeleted = false,
                Mail = data.Mail,
                Name = data.Name,
                Phone = data.Phone,
                TaxNumber = data.TaxNumber,
                Updated = DateTime.Now,
                UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Vote = 0,
                VoteCount = 0
            };
            context.Suppliers.Add(s);
            var savedItemCount = context.SaveChanges();
            if (savedItemCount > 0)
            {
                return s.Id;
            }
            return Guid.Empty;
        }

        public SupplierDto GetSupplier(Guid id)
        {
            return context.Suppliers.Where(s => s.Id == id).Select(s => new SupplierDto
            {
                Name = s.Name,
                Id = s.Id,
                Address = s.Address,
                Mail = s.Mail,
                Phone = s.Phone,
                TaxNumber = s.TaxNumber,
                Vote = s.Vote,
                VoteCount = s.VoteCount
            }).FirstOrDefault();
        }
    }
}
