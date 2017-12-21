using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.Mappers
{
    internal class ClientMapper : EntityTypeConfiguration<Client>
    {
        public ClientMapper()
        {
            Property(p => p.Id).IsRequired();
            Property(p => p.Secret).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.ApplicationType);
            Property(p => p.Active);
            Property(p => p.RefreshTokenLifeTime);
            Property(p => p.AllowedOrigin).IsRequired().HasMaxLength(100);

            ToTable("Clients");
        }
    }
}