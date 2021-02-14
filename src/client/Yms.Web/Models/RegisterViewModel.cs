using System.ComponentModel.DataAnnotations;

namespace Yms.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int Type { get; } = 1;
    }
}