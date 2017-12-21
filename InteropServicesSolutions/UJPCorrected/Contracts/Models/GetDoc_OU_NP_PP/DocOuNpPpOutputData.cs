using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.GetDoc_OU_NP_PP
{
    public class DocOuNpPpOutputData
    {
        public bool HasDocument { get; set; }
        public string Message { get; set; }
        public byte[] Document { get; set; }
    }
}
