using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InteropServices.CURM.Implementations
{
    [DataContract]
    public class ExecutedExportInput
    {
        [DataMember(IsRequired=true)]
        public long EDB { get; set; }

        [DataMember(IsRequired=false)]
        public Nullable<double> AmountOfExportFrom { get; set; }

        [DataMember(IsRequired = false)]
        public Nullable<double> AmountOfExportTo { get; set; }

        [DataMember(IsRequired = false)]
        public Nullable<int> MonthOfExportFrom { get; set; }

        [DataMember(IsRequired = false)]
        public Nullable<int> MonthOfExportTo { get; set; }

        [DataMember(IsRequired = false)]
        public Nullable<int> YearOfExportFrom { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<int> YearOfExportTo { get; set; }
    }
}
