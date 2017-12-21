using System.Collections.Generic;

namespace Interop.CC.Models.Helper
{
    // Опис: Класа која врши враќање на PagedCollection листа на податоци
    // Влезни параметри: апстрактен тип на објект/модел
    public class PagedCollection<T>
    {
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly long _totalSize;
        private readonly IReadOnlyCollection<T> _items;

        public PagedCollection()
        {
            
        }

        // Опис: Конструктор на класата PagedCollection
        // Влезни параметри: податочна вредност pageIndex, pageSize, totalSize, IReadOnlyCollection<T> items
        public PagedCollection(int pageIndex, int pageSize, long totalSize, IReadOnlyCollection<T> items)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _totalSize = totalSize;
            _items = items;
        }


        public int PageIndex { get { return _pageIndex; } }
        public long TotalSize { get { return _totalSize; } }
        public int PageSize { get { return _pageSize; } }

        public IReadOnlyCollection<T> Items
        {
            get { return _items; }
        }
    }
}
