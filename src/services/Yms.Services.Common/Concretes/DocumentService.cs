using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Yms.Common.Contracts;
using Yms.Data.Entities;
using Yms.Services.Common.Abstractions;

namespace Yms.Services.Common.Concretes
{
    class DocumentService : IDocumentService
    {
        private readonly DbContext context;
        private readonly IClaims claims;

        public DocumentService(DbContext context, IClaims claims)
        {
            this.context = context;
            this.claims = claims;
        }
        public string GetFileNameByDocumentId(Guid id)
        {
            var dbSet = context.Set<Document>();
            return dbSet.FirstOrDefault(f => f.Id == id)?.PhysicalAddress;
        }

        public Guid UploadFile(Stream stream, string fileName)
        {
            var id = Guid.NewGuid();
            var path = $"ftp://localhost/img/{id}_{fileName}";
            var request = (FtpWebRequest)WebRequest.Create(path);
            request.Credentials = new NetworkCredential("SERGEN", "sk05345802402");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;

            using (var ftpStream = request.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            var table = context.Set<Document>();
            table.Add(new Document
            {
                Id = id,
                IsActive = true,
                IsDeleted = false,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                CreatedBy = claims.Session.Id,
                UpdatedBy = claims.Session.Id,
                PhysicalAddress = $"{id}_{fileName}"
            });
            context.SaveChanges();
            return id;
        }
    }
}
