using System.ComponentModel.DataAnnotations;

namespace Yms.Api.Models
{
    public class LoginInputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}