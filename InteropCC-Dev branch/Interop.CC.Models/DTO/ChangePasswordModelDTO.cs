using System.ComponentModel.DataAnnotations;

namespace Interop.CC.Models.DTO
{
    public class ChangePasswordModelDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат")]
        public string ConfirmPassword { get; set; }
    }
}
