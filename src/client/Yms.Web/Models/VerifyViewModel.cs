namespace Yms.Web.Models
{
    public class VerifyViewModel : NewPasswordViewModel
    {
        public bool CodeExists { get; set; }
        public bool InvalidPassword { get; set; }
    }
}
