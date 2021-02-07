using System;
using Yms.Common.Contracts.Enums;

namespace Yms.Contracts.CommonServices
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string type { get; set; }
    }
}
