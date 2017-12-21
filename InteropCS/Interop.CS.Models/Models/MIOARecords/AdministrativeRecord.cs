using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.Models.MIOARecords
{
    public class AdministrativeRecord
    {
        public int Id { get; set; }
        public long ItemNumber { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateEntered { get; set; }
        public string ApplicantNameAddress { get; set; }
        public string ElectronicDBName { get; set; }
        public string ElectronicDBTypeVersion { get; set; }
        public string DataType { get; set; }
        public string LegislationData { get; set; }
        public string AuthorizedPersonData { get; set; }
        public string Note { get; set; }
        public string OptionalField1 { get; set; }
        public string OptionalField2 { get; set; }

        public virtual ICollection<AdministrativeRecordLog> AdministrativeRecordLogs { get; set; }

    }
}
