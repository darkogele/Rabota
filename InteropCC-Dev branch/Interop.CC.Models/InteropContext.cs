using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CC.Models
{
    public class InteropContext : IdentityDbContext<ApplicationUser>, IInteropContext
    {
        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<SoapFault> SoapFaults { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        
        public InteropContext()
            : base("connectionStringCC")
        {
          //  Database.SetInitializer<InteropContext>(new CreateDatabaseIfNotExists<InteropContext>());
            // git test
            
        }
         
        protected override void OnModelCreating(DbModelBuilder modelBuilder) // All configuration code using Fluent API should be in "OnModelCreating" method
        {
            // This method is called when the model for a context class MIMContext has ben initialized 
            // DbModelBuilder is a used to map CLR classes to a database schema

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
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
