using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.Mappers
{
    internal class MessageLogMapper : EntityTypeConfiguration<MessageLog>
    {
        public MessageLogMapper()
        {
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Consumer).IsRequired();
            Property(p => p.Provider);
            Property(p => p.RoutingToken).IsRequired();
            Property(p => p.Service).IsRequired();
            Property(p => p.ServiceMethod);
            Property(p => p.TransactionId).IsRequired();
            Property(p => p.Dir).IsRequired().HasMaxLength(50);
            Property(p => p.CallType).IsRequired().HasMaxLength(50);
            Property(p => p.PublicKey);
            Property(p => p.Status).HasMaxLength(50);
            Property(p => p.MimeType).HasMaxLength(100);
            Property(p => p.Timestamp).IsRequired();
            Property(p => p.TokenTimestamp);
            Property(p => p.Signature).IsRequired();
            Property(p => p.IsInteropTestCommunicationCall).IsRequired();

            ToTable("MessageLogs");
        }
    }
}
