using System.Collections.Generic;

namespace Contracts.Models.RegExcBonds
{
    public class ExciseBondsOutput
    {
        public List<ExciseBonds> ExciseBonds { get; set; }
        public List<ExciseGoods> ExciseGoods { get; set; }
        public List<ExciseSpaces> ExciseSpaces { get; set; }
    }

    public class ExciseBonds
    {
        public string id { get; set; }
        public string edb { get; set; }
        public string name { get; set; }
        public string vid_odobrenie { get; set; }
        public string broj_odobrenie { get; set; }
        public string datum_odobrenie { get; set; }
        public string prethodno_odobrenie { get; set; }
        public string sledno_odobrenie { get; set; }
        public string embg_odgovorno_lice { get; set; }
        public string naziv_odgovorno_lice { get; set; }
        public string embg_polnomosnik { get; set; }
        public string naziv_polnomosnik { get; set; }

    }
    public class ExciseGoods
    {
        public string id { get; set; }
        public string broj_odobrenie { get; set; }
        public string stavka { get; set; }
        public string vid { get; set; }
        public string proizvod { get; set; }
        public string tarifa { get; set; }
        public string kolicina { get; set; }
        public string opis_merka { get; set; }
        public string za_proizvodstvo { get; set; }
        public string za_skladiranje { get; set; }
    }
    public class ExciseSpaces
    {
        public string id { get; set; }
        public string broj_odobrenie { get; set; }
        public string stavka { get; set; }
        public string opstina { get; set; }
        public string naziv_opstina { get; set; }
        public string mesto { get; set; }
        public string naziv_mesto { get; set; }
        public string naziv_ulica { get; set; }
        public string ulica_broj { get; set; }
        public string za_proizvodstvo { get; set; }
        public string za_skladiranje { get; set; }
        public string embg_odgovorno_lice { get; set; }
        public string naziv_odgovorno_lice { get; set; }
        public string zabeleska { get; set; }
        public string status { get; set; }
        public string status_datum { get; set; }
    }
}
