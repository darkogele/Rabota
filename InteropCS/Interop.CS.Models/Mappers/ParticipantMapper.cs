using System.Data.Entity.ModelConfiguration;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    internal class ParticipantMapper : EntityTypeConfiguration<Participant>
    {
        public ParticipantMapper()
        {
            Property(p => p.Code).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.Uri).IsRequired();
            Property(p => p.IsActive).IsRequired();
            Property(p => p.PublicKey).IsRequired();

            ToTable("Participants")
                .HasKey(p => p.Code);
        }
    }
}
