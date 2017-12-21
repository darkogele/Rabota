using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class ListaNaPromeniDTO : BasicPrintInfoDTO
    {
        public byte ListType { get; set; }
        public string DateFrom { get; set; }
        public string InfoMessage { get; set; }

    }
}
