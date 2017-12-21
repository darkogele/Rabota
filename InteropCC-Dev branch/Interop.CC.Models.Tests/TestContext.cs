using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.Tests
{
    public class TestContext : IInteropContext
    {
        public TestContext()
        {
            Services = new TestDbSet<Service>(s => s.Code);
            MessageLogs = new TestDbSet<MessageLog>(s => s.Id);
            Services = new TestDbSet<Service>(s => s.Code);
            RefreshTokens = new TestDbSet<RefreshToken>(s => s.Id);
            Providers = new TestDbSet<Provider>(s => s.RoutingToken);
        }
        public DbSet<MessageLog> MessageLogs { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<SoapFault> SoapFaults { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public int SaveChangesCount { get; private set; }

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
