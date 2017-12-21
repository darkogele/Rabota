using System;
using System.Runtime.Serialization;

namespace Contracts.DataAccessLibrary.DataForExecutedExport
{
    [DataContract]
    public class ExecutedExportInput
    {
        [DataMember(IsRequired = true)]
        public long EDB { get; set; }

        [DataMember(IsRequired = false)]
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
