
namespace Interop.CC.Models.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IInteropContext _context;

        // Опис: Конструктор на класата UnitOfWork
        // Влезни параметри: IInteropContext context
        public UnitOfWork(IInteropContext context)
        {
            _context = context;
        }

        public IInteropContext Context
        {
            get { return _context; }
        }

        // Опис: Метод за зачувување на податоци во контекстот InteropContext
        // Влезни параметри: /
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Опис: Метод за ослободување на ресурсите за прикачени податоци на контекстот InteropContext
        // Влезни параметри: /
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
