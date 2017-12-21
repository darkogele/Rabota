using System;

namespace EvalServiceLibrary.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IEvalContext Context { get; }

        void SaveChanges();
    }
}
