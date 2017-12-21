using System.Collections.Generic;

namespace Interop.CS.Models.Helpers
{
    public class PagedCollection<T>
    {
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly long _totalSize;
        private readonly IReadOnlyCollection<T> _items;

        //Опис: Се користи за пагинација на страните 
        //Влезни параметри: индекс на страна, големина на страна, вкупен број на записи, записи
        
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