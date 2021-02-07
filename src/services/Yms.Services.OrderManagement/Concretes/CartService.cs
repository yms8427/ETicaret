using Microsoft.EntityFrameworkCore;
using System;
using Yms.Data.Entities;
using Yms.Services.OrderManagement.Abstractions;

namespace Yms.Services.OrderManagement.Concretes
{
    internal class CartService : ICartService
    {
        private readonly DbSet<Basket> table;
        private readonly DbContext context;

        public CartService(DbContext context)
        {
            table = context.Set<Basket>();
            this.context = context;
        }

        public bool Add(Guid userId, Guid productId, byte count)
        {
            table.Add(new Basket
            {
                IsActive = true,
                IsDeleted = false,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                CreatedBy = userId,
                UpdatedBy = userId,
                ProductId = productId,
                Amount = count,
                UserId = userId
            });
            return context.SaveChanges() > 0;
        }
    }
}

//TODO: Repository Pattern && Generic Repository Pattern && UnitOfWork Pattern