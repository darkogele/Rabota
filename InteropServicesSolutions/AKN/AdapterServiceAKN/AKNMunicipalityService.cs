using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AdapterServiceAKN
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class AKNMunicipalityService : IAKNMunicipality
    {
        public IEnumerable<MunicipalityDTO> GetMunicipalities()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var AKNClient = new AKNOriginalService.Service_MACEDONIAN_CADASTRESoapClient();
            DataSet municipalities = AKNClient.VRATIOPSDATASET();

            IEnumerable<MunicipalityDTO> municipalityList;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                municipalities.WriteXml(xmlTextWriter);
                xmlTextWriter.Flush();
                var stringDoc = stringWriter.GetStringBuilder().ToString();
                XDocument xmlDoc = XDocument.Parse(stringDoc);

                municipalityList = xmlDoc.Root.Elements("OPSTINI").Select(x => new MunicipalityDTO()
                {
                    Name = (string)x.Element("NAZIV"),
                    Value = (int)x.Element("OPS")
                });

            }
            return municipalityList;
        }
        public IEnumerable<MunicipalityDTO> GetCMunicipalities(string municipalityValue)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var AKNClient = new AKNOriginalService.Service_MACEDONIAN_CADASTRESoapClient();
            DataSet municipalities = AKNClient.VRATIKATOPSDATASET(municipalityValue);

            IEnumerable<MunicipalityDTO> caadstralMunicipalityList;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                municipalities.WriteXml(xmlTextWriter);
                xmlTextWriter.Flush();
                var stringDoc = stringWriter.GetStringBuilder().ToString();
                XDocument xmlDoc = XDocument.Parse(stringDoc);

                caadstralMunicipalityList = xmlDoc.Root.Elements("KATOPSTINI").Select(x => new MunicipalityDTO()
                {
                    Name = (string)x.Element("NAZIV"),
                    Value = (int)x.Element("KOPS")
                });

            }
            return caadstralMunicipalityList;
        }
    }
}
