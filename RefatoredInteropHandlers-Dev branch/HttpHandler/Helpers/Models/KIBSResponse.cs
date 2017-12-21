using System;

namespace Helpers.Models
{
    public  class KIBSResponse
    {
        public KIBSResponse()
        {
            IsSuccessful = true;
        }
        public string Hash { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
