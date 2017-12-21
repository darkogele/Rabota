using System.Collections.Generic;

namespace Interop.CC.Models.DTO.Institutions
{
    public class TekovnaSostojbaAKNDTO : BasicPrintInfoDTO
    {
        public List<CVLEInfo> Info { get; set; }
        public List<CVUnits> Units { get; set; }
        public List<CVActors> Actors { get; set; }
        public List<CVOwners> Owners { get; set; }
        public List<CVActivities> Activities { get; set; }
        public List<CVMembership> Membership { get; set; }
        public List<CVFounding> Founding { get; set; }
        public string Message { get; set; }
        public string TekovnaSostojbaXML { get; set; }
        public string TekovnaSostojbaPDF { get; set; }
    }
  
}
