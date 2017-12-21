using System;

namespace Interop.CC.Models.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IInteropContext Context { get; }

        void SaveChanges();
    }
}
