using System.Data.Entity.ModelConfiguration;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    internal class ClientMapper : EntityTypeConfiguration<Client>
    {
        public ClientMapper()
        {
            HasKey(p => p.Id);
            Property(p => p.Secret).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.ApplicationType);
            Property(p => p.Active);
            Property(p => p.RefreshTokenLifeTime);
            Property(p => p.AllowedOrigin).HasMaxLength(100);
        }
    }
}

