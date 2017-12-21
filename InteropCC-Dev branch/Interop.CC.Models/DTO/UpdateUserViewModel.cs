using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO
{
    public class UpdateUserViewModel : ManagerUserViewModel
    {
        public string Oldpassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }
        public string[] SelectedServiceRoles1 { get; set; }

    }
}
