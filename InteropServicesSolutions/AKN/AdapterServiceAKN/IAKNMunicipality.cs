using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IAKNMunicipality
    {
        [OperationContract]
        IEnumerable<MunicipalityDTO> GetMunicipalities();
        [OperationContract]
        IEnumerable<MunicipalityDTO> GetCMunicipalities(string municipalityValue);
    }
}
