using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Yms.Common.Contracts.Enums;
using Yms.Contracts.CommonServices;
using Yms.Data.Entities;
using Yms.Services.Common.Abstractions;

namespace Yms.Services.Common.Concretes
{
    class UserService : IUserService
    {
        private readonly DbSet<User> users;
        private readonly DbContext context;

        public UserService(DbContext context)
        {
            users = context.Set<User>();
            this.context = context;
        }

        public UserInfoDto Authenticate(string username, string password)
        {
            var hash = ComputeHash(password);
            var user = users.Single(f => f.UserName == username && f.Password == hash);
            if (!user.IsActive)
            {
                return null;
            }
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

        public IEnumerable<UserDto> GetUsers()
        {
            return users.Select(u => new UserDto
            {
                DisplayName = u.DisplayName,
                Id = u.Id,
                Mail = u.MailAddress,
                Phone = u.Phone,
                type = u.Type.ToString()
            }).ToList();
        }

        public (bool isSuccess, Guid userId, string code) Register(NewUserDto newUser)
        {
            var verificationCode = $"{Guid.NewGuid():N}{Guid.NewGuid().ToString("N")}".Substring(0, 39);
            var _new = new User
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Phone = newUser.Phone,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                DisplayName = newUser.DisplayName,
                IsActive = false,
                IsDeleted = false,
                MailAddress = newUser.Mail,
                Password = "NEWUSER",
                Type = newUser.Type,
                Updated = DateTime.Now,
                UserName = newUser.UserName,
                VerificationCode = verificationCode
            };
            users.Add(_new);
            var isSuccess = context.SaveChanges() > 0;
            return (isSuccess, _new.Id, verificationCode);
        }
    }
}
