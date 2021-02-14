using System;

namespace Yms.Services.Common.Abstractions
{
    public interface IMailService
    {
        void SendVerificationMail(Guid userId, string content);
    }
}