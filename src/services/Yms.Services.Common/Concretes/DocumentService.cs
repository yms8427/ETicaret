using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yms.Data.Entities;
using Yms.Services.Common.Abstractions;

namespace Yms.Services.Common.Concretes
{
    class DocumentService : IDocumentService
    {
        private readonly DbContext context;

        public DocumentService(DbContext context)
        {
            this.context = context;
        }
        public string GetFileNameByDocumentId(Guid id)
        {
            var dbSet = context.Set<Document>();
            return dbSet.FirstOrDefault(f => f.Id == id)?.PhysicalAddress;
        }
    }
}
