using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.Mappers
{
    public class BusesMapper : EntityTypeConfiguration<Buses>
    {
        public BusesMapper()
        {
            Property(p => p.Code).IsRequired();
            Property(p => p.Url);
            ToTable("Buses").HasKey(p => new {p.Code});
        }
    }
}
