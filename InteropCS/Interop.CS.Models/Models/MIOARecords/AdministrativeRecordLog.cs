using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.Models.MIOARecords
{
    public class AdministrativeRecordLog
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public string UserName { get; set; }
        public string PerformedActivity { get; set; }
        public int AdministrativeRecordId { get; set; }
        public virtual AdministrativeRecord AdministrativeRecord { get; set; }
    }
}
