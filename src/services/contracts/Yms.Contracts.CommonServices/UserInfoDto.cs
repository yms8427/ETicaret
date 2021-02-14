using System;

namespace Yms.Contracts.CommonServices
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }

    public class AuthenticationResultDto
    {
        public UserInfoDto Info { get; set; }
        public AuthenticationType Result { get; set; }
    }

    public enum AuthenticationType
    {
        Success,
        Passive,
        WaitsVerification,
        PasswordExpired
    }
}