using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Contracts.DTO_s.AKNMunicipalityService;
using Contracts.Interfaces.IAKNMunicipality;
using Contracts.AKNMunicipalities;

namespace Implementations.Test.Implementations.AKNMunicipalityService
{
    public class AKNMunicipalityService : IAKNMunicipality
    {
        public IEnumerable<MunicipalityDTO> GetMunicipalities()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();
            DataSet municipalities = aknClient.VRATIOPSDATASET();

            IEnumerable<MunicipalityDTO> municipalityList;
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                municipalities.WriteXml(xmlTextWriter);
                xmlTextWriter.Flush();
                var stringDoc = stringWriter.GetStringBuilder().ToString();
                XDocument xmlDoc = XDocument.Parse(stringDoc);

                municipalityList = xmlDoc.Root.Elements("OPSTINI").Select(x => new MunicipalityDTO
                {
                    Name = (string)x.Element("NAZIV"),
                    Value = (int)x.Element("OPS")
                });

            }
            return municipalityList;
        }

        public IEnumerable<MunicipalityDTO> GetCMunicipalities(string municipalityValue)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var aknClient = new Service_MACEDONIAN_CADASTRESoapClient();

            DataSet municipalities = aknClient.VRATIKATOPSDATASET(municipalityValue);

            IEnumerable<MunicipalityDTO> cadastralMunicipalityList;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                municipalities.WriteXml(xmlTextWriter);
                xmlTextWriter.Flush();
                var stringDoc = stringWriter.GetStringBuilder().ToString();
                XDocument xmlDoc = XDocument.Parse(stringDoc);

                cadastralMunicipalityList = xmlDoc.Root.Elements("KATOPSTINI").Select(x => new MunicipalityDTO()
                {
                    Name = (string)x.Element("NAZIV"),
                    Value = (int)x.Element("KOPS")
                });

            }
            return cadastralMunicipalityList;
        }
    }
}
