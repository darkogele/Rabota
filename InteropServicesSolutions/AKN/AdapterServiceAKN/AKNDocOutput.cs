using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    public class AKNDocOutput
    {
        public bool HasDocument { get; set; }
        public string Message { get; set; }
        public byte[] Document { get; set; }
    }
}
