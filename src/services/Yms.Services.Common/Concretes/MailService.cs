using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Yms.Common.Contracts;
using Yms.Data.Entities;
using Yms.Services.Common.Abstractions;

namespace Yms.Services.Common.Concretes
{
    internal class MailService : IMailService
    {
        private readonly DbSet<User> users;
        private readonly Settings.MailConfiguration mailConfiguration;
        public MailService(DbContext context, IOptions<Settings> options)
        {
            mailConfiguration = options.Value.Mail;
            users = context.Set<User>();
        }
        public void SendVerificationMail(Guid userId, string content)
        {
            var mailAddress = users.Where(f => f.Id == userId && !f.IsActive)
                            .Select(i => i.MailAddress)
                            .FirstOrDefault();
            if (string.IsNullOrEmpty(mailAddress))
            {
                return;
            }

            var client = new SmtpClient
            {
                Host = mailConfiguration.Server,
                Timeout = 30000,
                Credentials = new NetworkCredential(mailConfiguration.User, mailConfiguration.Password),
                EnableSsl = true,
                Port = 587
            };
            var mail = new MailMessage(new MailAddress(mailConfiguration.User, "YMS Alışveriş Dünyası"), 
                                       new MailAddress(mailAddress))
            {
                Body = content,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = "YMS Yeni Üyelik",
                SubjectEncoding = Encoding.UTF8
            };
            client.Send(mail);
        }
    }
}
