using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class DataForPropertyListDTO : BasicPrintInfoDTO
    {
        public string Municipality { get; set; }
        public string CadastralMunicipality { get; set; }
        public string PropertyList { get; set; }
        public List<Loads> LoadsList { get; set; }
        public List<Objects> ObjectsList { get; set; }
        public List<Owner> OwnersList { get; set; }
        public List<Parcel> ParcelsList { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string PropertylistPDF { get; set; }
        public string PropertylistXML { get; set; }
        
        
    }
    public class Loads
    {
        public string Text { get; set; }
    }
    public class Objects
    {
        public string Number { get; set; }
        public int Object { get; set; }
        public string Entry { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
        public string Purpose { get; set; }
        public string Location { get; set; }
        public long Grounds { get; set; }
        public string YearBuilt { get; set; }
        public string Pravo { get; set; }
        public string OsnovGradba { get; set; }
    }
    public class Owner
    {
        public string PersonalNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Part { get; set; }

    }
    public class Parcel
    {
        public string PartNumber { get; set; }
        public int ObjectParcel { get; set; }
        public string Location { get; set; }
        public string Culture { get; set; }
        public string Class { get; set; }
        public long Grounds { get; set; }
        public string Pravo { get; set; }
    }
}
