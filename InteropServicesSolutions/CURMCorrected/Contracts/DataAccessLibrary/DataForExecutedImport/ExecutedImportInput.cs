using System;
using System.Runtime.Serialization;

namespace Contracts.DataAccessLibrary.DataForExecutedImport
{
    [DataContract]
    public class ExecutedImportInput
    {
        [DataMember(IsRequired = true)]
        public long EDB { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<double> AmountOfImportFrom { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<double> AmountOfImportTo { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<double> AmountOfImportTaxFrom { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<double> AmountOfImportTaxTo { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<int> MonthOfImportFrom { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<int> MonthOfImportTo { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<int> YearOfImportFrom { get; set; }
        [DataMember(IsRequired = false)]
        public Nullable<int> YearOfImportTo { get; set; }
    }
}
