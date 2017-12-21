namespace EvalServiceLibrary.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEvalContext _context;
        public UnitOfWork(IEvalContext context)
        {
            _context = context;
        }

        public IEvalContext Context
        {
            get { return _context; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
