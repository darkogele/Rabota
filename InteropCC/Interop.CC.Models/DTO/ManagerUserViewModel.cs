using Interop.CC.Models.Models;
using System.Collections.Generic;

namespace Interop.CC.Models.DTO
{
    public class ManagerUserViewModel
    {
        public ManagerUserViewModel()
        {
            ServiceRoles = new List<RoleViewModel>();
            UserRoles = new List<RoleViewModel>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public List<RoleViewModel> ServiceRoles { get; set; }//15
        public List<RoleViewModel> UserRoles { get; set; }//3
    }
}
