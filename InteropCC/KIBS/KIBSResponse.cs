using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIBS
{
    
  public  class KIBSResponse
    {
      public KIBSResponse()
      {
          IsSuccessful = true;
      }
        public string  Hash { get; set; }
        public DateTime TimeStamp { get; set; }
      public bool IsSuccessful { get; set; }
    }

}
