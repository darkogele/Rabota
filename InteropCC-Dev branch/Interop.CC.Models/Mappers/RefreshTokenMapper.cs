using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.Mappers
{
    internal class RefreshTokenMapper : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenMapper()
        {
            HasKey(t => t.Id);
            Property(p => p.Subject).IsRequired().HasMaxLength(50);
            Property(p => p.ClientId).IsRequired().HasMaxLength(50);
            Property(p => p.IssuedUtc);
            Property(p => p.ExpiresUtc);
            Property(p => p.ProtectedTicket).IsRequired();

            ToTable("RefreshTokens");
        }
    }
}