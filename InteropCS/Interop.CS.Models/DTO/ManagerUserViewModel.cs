using System.Collections.Generic;

namespace Interop.CS.Models.DTO
{
    public class ManagerUserViewModel
    {
        public ManagerUserViewModel()
        {
            Roles = new List<string>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool CanBeModified { get; set; }
    }
}
