using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Common.Caching.Abstractions;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    public class SupplierService : ISupplierService
    {
        private readonly YmsDbContext context;
        private readonly ICacheManager cacheManager;

        public SupplierService(YmsDbContext context, ICacheManager cacheManager)
        {
            this.context = context;
            this.cacheManager = cacheManager;
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
                var dto = new SupplierDto
                {
                    Id = s.Id,
                    Address = s.Address,
                    Mail = s.Mail,
                    Name = s.Name,
                    Phone = s.Phone,
                    TaxNumber = s.TaxNumber,
                    Vote = s.Vote,
                    VoteCount = s.VoteCount
                };
                cacheManager.Update<List<SupplierDto>>(nameof(Supplier), (cachedData) =>
                {
                    cachedData.Add(dto);
                });
                return s.Id;
            }

            return Guid.Empty;
        }

        public SupplierDto GetSupplier(Guid id)
        {
            return context.Suppliers.Where(s => s.Id == id && !s.IsDeleted && s.IsActive).Select(s => new SupplierDto
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

        public string GetSupplierNameById(Guid id)
        {
            return context.Suppliers.FirstOrDefault(f => f.Id == id)?.Name;
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            return cacheManager.GetOrCreate<List<SupplierDto>>(nameof(Supplier), () => FetchSuppliers());
            
        }

        private List<SupplierDto> FetchSuppliers()
        {
            return context.Suppliers.Where(f => !f.IsDeleted && f.IsActive)
                                    .OrderByDescending(s => s.Created)
                                    .Select(s => new SupplierDto
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

        public bool Remove(Guid id)
        {
            var supplier = context.Suppliers.SingleOrDefault(f => f.Id == id);
            supplier.IsDeleted = true;
            var isDeleted = context.SaveChanges() > 0;
            if (isDeleted)
            {
                cacheManager.Update<List<SupplierDto>>(nameof(Supplier), (cachedData) =>
                {
                    var data = cachedData.FirstOrDefault(f => f.Id == id);
                    if (data != null)
                    {
                        cachedData.Remove(data);
                    }
                });
                return true;
            }
            return false;
        }
    }
}
