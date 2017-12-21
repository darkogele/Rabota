using System.Data.Entity.ModelConfiguration;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.Mappers
{
    public class ProvidersMapper : EntityTypeConfiguration<Provider>
    {
        public ProvidersMapper()
        {
            HasKey(t => t.RoutingToken);
            Property(p => p.PublicKey).IsRequired();

            ToTable("Providers");
        }
    }
}
