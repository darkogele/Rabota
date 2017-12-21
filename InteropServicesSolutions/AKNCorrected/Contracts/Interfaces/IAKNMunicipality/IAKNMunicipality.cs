using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DTO_s.AKNMunicipalityService;

namespace Contracts.Interfaces.IAKNMunicipality
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNMunicipality
    {
        [OperationContract]
        IEnumerable<MunicipalityDTO> GetMunicipalities();

        [OperationContract]
        IEnumerable<MunicipalityDTO> GetCMunicipalities(string municipalityValue);
    }
}
