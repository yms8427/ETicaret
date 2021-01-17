using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yms.Common.Contracts.Enums;
using Yms.Data.Entity.Abstractions;

namespace Yms.Data.Entities
{
    [Table("Users", Schema = "Management")]
    public class User : EntityBase
    {
        [Required]
        [MaxLength(40)]
        public string UserName { get; set; }
        [MaxLength(60)]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        [Required]
        public UserType Type { get; set; }
        [MaxLength(40)]
        public string VerificationCode { get; set; }
    }
}
