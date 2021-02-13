using Yms.Common.Contracts.Enums;

namespace Yms.Contracts.CommonServices
{
    public class NewUserDto
    {
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public UserType type { get; set; }

    }
}
