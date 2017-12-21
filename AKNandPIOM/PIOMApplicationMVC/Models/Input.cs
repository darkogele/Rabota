using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIOMApplicationMVC.Models
{
    public class Input
    {
        
        [Required, DisplayName("Корисничко Име")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Лозинка")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Општина")]
        public string Municipality { get; set; }
        [Required]
        [DisplayName("Катастарска Општина")]
        public string CadastralMunicipality { get; set; }
        [Required]
        [DisplayName("Број")]
        public string PropertyList { get; set; }
        
    }
}