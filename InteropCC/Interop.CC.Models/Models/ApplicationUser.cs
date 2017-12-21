using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Certificate { get; set; }
    }
}
