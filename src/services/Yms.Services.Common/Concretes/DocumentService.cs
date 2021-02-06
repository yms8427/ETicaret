using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Yms.Contracts.CommonServices;
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

    class UserService : IUserService
    {
        private readonly DbSet<User> set;
        public UserService(DbContext context)
        {
            set = context.Set<User>();
        }

        public UserInfoDto Authenticate(string username, string password)
        {
            var hash = ComputeHash(password);
            var user = set.Single(f => f.UserName == username && f.Password == hash);
            return new UserInfoDto
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName
            };
        }

        private static string ComputeHash(string text)
        {
            var prefix = "_74YudhT63Hp0f!";
            var crypt = new SHA256Managed();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(prefix + text));
            var builder = new StringBuilder();
            foreach (byte theByte in crypto)
            {
                builder.Append(theByte.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
