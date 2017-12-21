using Interop.CC.Models.Models;
using System.Data.Entity.ModelConfiguration;

namespace Interop.CC.Models.Mappers
{
    public class SoapFaultMapper : EntityTypeConfiguration<SoapFault>
    {
        public SoapFaultMapper()
        {
            HasKey(p => p.TransactionId);
            Property(p => p.Code).IsRequired();
            Property(p => p.DateOccured).IsRequired();
            Property(p => p.Details).IsRequired();
            Property(p => p.Reason).IsRequired();
            Property(p => p.SubCode).IsRequired();
            Property(p => p.DateCreated).IsRequired();
            
            ToTable("SoapFaults");
        }
    }
}
