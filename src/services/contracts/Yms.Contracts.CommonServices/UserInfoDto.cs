using System;

namespace Yms.Contracts.CommonServices
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}