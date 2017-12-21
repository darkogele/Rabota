using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using Interop.CS.Models.Models.MIOARecords;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CS.Models
{
    public class InteropContext : IdentityDbContext<ApplicationUser>, IInteropContext
    {
       
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

        public InteropContext()
            : base("CSInterop")
        {
            //  Database.SetInitializer<InteropContext>(new CreateDatabaseIfNotExists<InteropContext>());
            // git test

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            // All configuration code using Fluent API should be in "OnModelCreating" method
        {
            // This method is called when the model for a context class MIMContext has ben initialized 
            // DbModelBuilder is a used to map CLR classes to a database schema

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                               && type.BaseType.GetGenericTypeDefinition() == typeof (EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

        }
    }
}
