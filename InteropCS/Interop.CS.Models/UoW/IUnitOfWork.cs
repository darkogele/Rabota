using System;
using System.Threading.Tasks;

namespace Interop.CS.Models.UoW
{
    public interface IUnitOfWork
    {
        IInteropContext Context { get; }
        void SaveChanges();
        void SaveChangesAsync();
     }
}
