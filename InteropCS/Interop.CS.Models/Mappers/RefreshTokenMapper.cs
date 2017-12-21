using System.Data.Entity.ModelConfiguration;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    internal class RefreshTokenMapper : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenMapper()
        {
            HasKey(p => p.Id);
            Property(p => p.Subject).IsRequired().HasMaxLength(50);
            Property(p => p.ClientId).IsRequired().HasMaxLength(50);
            Property(p => p.IssuedUtc);
            Property(p => p.ExpiresUtc);
            Property(p => p.ProtectedTicket).IsRequired();
        }
    }
}
