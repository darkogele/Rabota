using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using Interop.CS.Models.Tests.HelperException;
using Interop.CS.Models.Models.MIOARecords;

namespace Interop.CS.Models.Tests
{
    public class TestContext : IInteropContext
    {

        public TestContext()
        {
            Participants = new TestDbSet<Participant>(p => p.Code);
            AccessMappings = new TestDbSet<AccessMapping>(p => p.ConsumerCode, p => p.ProviderCode, p => p.ServiceCode);
            Services = new TestDbSet<CSService>(s => s.Code, s => s.ParticipantCode);
            MessageLogs = new TestDbSet<MessageLog>(s => s.Id);
            Buses = new TestDbSet<Buses>(s => s.Code);
            SoapFaults = new TestDbSet<SoapFault>(s => s.TransactionId);
            Clients = new TestDbSet<Client>(s => s.Id);
            RefreshTokens = new TestDbSet<RefreshToken>(s => s.Id);
            MessageLogStatistic = new TestDbSet<MessageLogStatistic>(s => s.Id);
            AdministrativeRecords = new TestDbSet<AdministrativeRecord>(s => s.Id);
            AdministrativeServices = new TestDbSet<AdministrativeService>(s => s.Id);
            AdministrativeRecordLogs = new TestDbSet<AdministrativeRecordLog>(s => s.Id);
            AdministrativeServiceLogs = new TestDbSet<AdministrativeServiceLog>(s => s.Id);
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<CSService> Services { get; set; }
        public DbSet<AccessMapping> AccessMappings { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<Buses> Buses { get; set; }
        public DbSet<SoapFault> SoapFaults { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<MessageLogStatistic> MessageLogStatistic { get; set; }
        public DbSet<AdministrativeRecord> AdministrativeRecords { get; set; }
        public DbSet<AdministrativeService> AdministrativeServices { get; set; }
        public DbSet<AdministrativeRecordLog> AdministrativeRecordLogs { get; set; }
        public DbSet<AdministrativeServiceLog> AdministrativeServiceLogs { get; set; }

        public int SaveChangesCount { get; private set; }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new UpdateSuccessfull();
        }

        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return 1;
        }
    }
}
