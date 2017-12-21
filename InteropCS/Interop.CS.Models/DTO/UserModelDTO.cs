using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CS.Models.DTO
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
    }
}
