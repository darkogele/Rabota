
using System.ComponentModel.DataAnnotations;

namespace Interop.CC.Models.DTO
{
    public class UserModelDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат")]
        public string ConfirmPassword { get; set; }
        public string PublicKey { get; set; }
        public string UserRole { get; set; }
        public string[] SelectedServiceRoles { get; set; }
    }
}
