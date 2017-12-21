using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    public class ATRparceli
    {
        public string message { get; set; }
        public List<ParcelAtr> nizpar { get; set; }
    }
    public class ParcelAtr
    {
        public string ops { get; set; }
        public string kops { get; set; }
        public string ilist { get; set; }
        public string broj_del { get; set; }
        public int objekt { get; set; }
        public string mesto { get; set; }
        public string kultura { get; set; }
        public long povrsina { get; set; }
        public string pravo { get; set; }
    }
}
