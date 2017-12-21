using Interop.CC.Models.Models;
using System.Data.Entity.ModelConfiguration;

namespace Interop.CC.Models.Mappers
{
    public class ServiceMapper: EntityTypeConfiguration<Service>
    {
        public ServiceMapper()
        {
            //Key
            HasKey(t => t.Code);  

            //Property(p => p.Code).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired();
            Property(p => p.Endpoint).IsRequired().HasMaxLength(100); ;
            Property(p => p.Wsdl).IsRequired();
            
            ToTable("Services");
        }
    }
}
