using System.Data.Entity;
using EvalServiceLibrary.Model;

namespace EvalServiceLibrary
{
    public interface IEvalContext
    {
        DbSet<EvalDTO> Evals { get; set; }

        int SaveChanges();

        void Dispose();
    }
}
