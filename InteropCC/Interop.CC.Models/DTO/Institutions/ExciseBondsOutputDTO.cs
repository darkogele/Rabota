using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class ExciseBondsOutputDTO : BasicPrintInfoDTO
    {
        public List<ExciseBondsDTO> ExciseBonds { get; set; }
        public List<ExciseGoodsDTO> ExciseGoods { get; set; }
        public List<ExciseSpacesDTO> ExciseSpaces { get; set; }
        public string Message { get; set; }
        public string RegistarPDF { get; set; }
        public string RegistarXML { get; set; }
    }

    public class ExciseBondsDTO
    {
        public string Id { get; set; }
        public string Edb { get; set; }
        public string Name { get; set; }
        public string Vid_odobrenie { get; set; }
        public string Broj_odobrenie { get; set; }
        public string Datum_odobrenie { get; set; }
        public string Prethodno_odobrenie { get; set; }
        public string Sledno_odobrenie { get; set; }
        public string Embg_odgovorno_lice { get; set; }
        public string Naziv_odgovorno_lice { get; set; }
        public string Embg_polnomosnik { get; set; }
        public string Naziv_polnomosnik { get; set; }
    }
    public class ExciseGoodsDTO
    {
        public string Id { get; set; }
        public string Broj_odobrenie { get; set; }
        public string Stavka { get; set; }
        public string Vid { get; set; }
        public string Proizvod { get; set; }
        public string Tarifa { get; set; }
        public string Kolicina { get; set; }
        public string Opis_merka { get; set; }
        public string Za_proizvodstvo { get; set; }
        public string Za_skladiranje { get; set; }
    }
    public class ExciseSpacesDTO
    {
        public string Id { get; set; }
        public string Broj_odobrenie { get; set; }
        public string Stavka { get; set; }
        public string Opstina { get; set; }
        public string Naziv_opstina { get; set; }
        public string Mesto { get; set; }
        public string Naziv_mesto { get; set; }
        public string Naziv_ulica { get; set; }
        public string Ulica_broj { get; set; }
        public string Za_proizvodstvo { get; set; }
        public string Za_skladiranje { get; set; }
        public string Embg_odgovorno_lice { get; set; }
        public string Naziv_odgovorno_lice { get; set; }
        public string Zabeleska { get; set; }
        public string Status { get; set; }
        public string Status_datum { get; set; }
    }
}
