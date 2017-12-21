
using System;
using System.Threading.Tasks;

namespace Interop.CS.Models.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IInteropContext _context;

        public UnitOfWork(IInteropContext context)
        {
            _context = context;
        }

        public IInteropContext Context
        {
            get { return _context; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

    }
}
