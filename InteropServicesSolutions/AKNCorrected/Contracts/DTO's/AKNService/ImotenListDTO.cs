using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO_s.AKNService
{
    public class dzgr
    {
        public string ops { get; set; }
        public string kops { get; set; }
        public string ilist { get; set; }
        public List<Loads> niztov { get; set; }
        public List<Objects> nizobj { get; set; }
        public List<Owner> nizsop { get; set; }
        public List<Parcel> nizpar { get; set; }
        public string data { get; set; }
        public string message { get; set; }

    }
    public class Loads
    {
        public string text { get; set; }
    }
    public class Objects
    {
        public string broj { get; set; }
        public int objekt { get; set; }
        public string vlez { get; set; }
        public string kat { get; set; }
        public string stan { get; set; }
        public string namena { get; set; }
        public string mesto { get; set; }
        public long povrsina { get; set; }
        public string godinagradba { get; set; }
        public string osnov { get; set; }
        public string pravo { get; set; }
    }
    public class Owner
    {
        public string embg { get; set; }
        public string ime { get; set; }
        public string mesto { get; set; }
        public string ulica { get; set; }
        public string broj { get; set; }
        public string del { get; set; }

    }
    public class Parcel
    {
        public string broj_del { get; set; }
        public int objekt { get; set; }
        public string mesto { get; set; }
        public string kultura { get; set; }
        public string klasa { get; set; }
        public long povrsina { get; set; }
        public string pravo { get; set; }
    }
}
