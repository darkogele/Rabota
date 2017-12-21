using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CC.Models.DTO
{
    public class ApplicationUser:IdentityUser
    {
        public string PublicKey { get; set; }
    }
}
