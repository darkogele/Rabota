using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using EvalServiceLibrary.Model;

namespace EvalServiceLibrary
{
    public class EvalContext : DbContext
    {
        public DbSet<EvalDTO> Evals { get; set; }

        public EvalContext()
            : base("connectionStringCCEval")
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
