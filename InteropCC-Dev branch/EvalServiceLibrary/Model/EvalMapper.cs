using System.Data.Entity.ModelConfiguration;

namespace EvalServiceLibrary.Model
{
    public class EvalMapper : EntityTypeConfiguration<EvalDTO>
    {
        public EvalMapper()
        {
            HasKey(t => t.Id);

            Property(p => p.Submitter).IsRequired();
            Property(p => p.Comments);
            Property(p => p.TimeSubmitted).IsRequired();

            ToTable("Evals");
        }
    }
}
