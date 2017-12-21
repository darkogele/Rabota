using System.Collections.Generic;
using System.ServiceModel;
using BusSyncMockWCFService.DAL;

namespace BusSyncMockWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBusSyncMockService
    {

        [OperationContract]
        IEnumerable<Participant> GetParticipants();
        [OperationContract]
        IEnumerable<Service> GetServices();
    }
}
