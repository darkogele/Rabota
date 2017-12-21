using System.Data.Entity.ModelConfiguration;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    internal class ServiceMapper : EntityTypeConfiguration<CSService>
    {
        public ServiceMapper()
        {
            Property(p => p.Code).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.ParticipantCode).IsRequired();
            Property(p => p.Wsdl).IsRequired();

            ToTable("CSServices")
                .HasKey(p => new { p.Code ,p.ParticipantCode});
        }
    }
}
