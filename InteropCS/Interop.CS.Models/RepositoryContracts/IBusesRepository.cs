using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IBusesRepository: IDisposable
    {
        IEnumerable<Buses> GetBuses();
        string GetBusUrl(string busCode);
        PagedCollection<Buses> GetBusesPaged(int pageIndex, int itemsPerPage);
        void CreateBus(Buses bus);
    }
}
