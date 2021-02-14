using System.ComponentModel.DataAnnotations;

namespace Yms.Web.Models
{
    public class NewPasswordViewModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor")]
        public string PasswordRepeat { get; set; }
        public string Code { get; set; }
    }
}
