using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using Interop.CS.Models.Models.MIOARecords;

namespace Interop.CS.Models
{
    public interface IInteropContext: IDisposable
    {
        DbSet<Participant> Participants { get; set; }
        DbSet<CSService> Services { get; set; }
        DbSet<AccessMapping> AccessMappings { get; set; }
        DbSet<MessageLog> MessageLogs { get; set; }
        DbSet<Buses> Buses { get; set; }
        DbSet<SoapFault> SoapFaults { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
        DbSet<MessageLogStatistic> MessageLogStatistic { get; set; }
        DbSet<AdministrativeRecord> AdministrativeRecords { get; set; }
        DbSet<AdministrativeService> AdministrativeServices { get; set; }
        DbSet<AdministrativeRecordLog> AdministrativeRecordLogs { get; set; }
        DbSet<AdministrativeServiceLog> AdministrativeServiceLogs { get; set; }
    }
}
