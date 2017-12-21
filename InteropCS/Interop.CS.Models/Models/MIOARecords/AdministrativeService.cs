using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.Models.MIOARecords
{
    public class AdministrativeService
    {
        public int Id { get; set; }
        public long ItemNumber { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateEntered { get; set; }
        public string ApplicantNameAddress { get; set; }
        public string AdministrativeServiceName { get; set; }
        public string AdministrativeServiceDesc { get; set; }
        public string LegislationForAdminService { get; set; }
        public string LegislationForAdminServicePayment { get; set; }
        public string AdminServicePaymentAmount { get; set; }
        public string AdminServiceSubmissionLeagalDeadline { get; set; }
        public string DeliveringSpecialFormData { get; set; }
        public string PreviousSubmittedDocDependencyData { get; set; }
        public string ElectronicDBTypeVersion { get; set; }
        public string AuthorizedPersonData { get; set; }
        public string Note { get; set; }
        public string OptionalField1 { get; set; }
        public string OptionalField2 { get; set; }
        public virtual ICollection<AdministrativeServiceLog> AdministrativeServiceLogs { get; set; }
    }
}
