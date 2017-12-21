using System.Data.Entity.ModelConfiguration;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    internal class AccessMappingMapper : EntityTypeConfiguration<AccessMapping>
    {
        public AccessMappingMapper()
        {
            Property(p => p.ProviderCode).IsRequired();
            Property(p => p.ConsumerCode).IsRequired();
            Property(p => p.ServiceCode).IsRequired();
            Property(p => p.MethodCode).IsRequired();
            Property(p => p.ProviderBusCode).IsRequired();
            Property(p => p.ConsumerBusCode).IsRequired();
            Property(p => p.IsActive).IsRequired();
            
            ToTable("AccessMappings")
                .HasKey(p => new { p.ProviderCode, p.ConsumerCode, p.ServiceCode, p.MethodCode, p.ProviderBusCode, p.ConsumerBusCode });
        }
    }
}
