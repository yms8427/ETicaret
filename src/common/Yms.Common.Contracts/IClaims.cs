using System;
using System.Collections.Generic;
using System.Text;

namespace Yms.Common.Contracts
{
    public interface IClaims
    {
        bool IsAuthenticated { get; set; }
        UserSessionData Session { get; set; }
    }

    public class UserClaims : IClaims
    {
        public bool IsAuthenticated { get; set; }
        public UserSessionData Session { get; set; }
    }

    public class UserSessionData
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
