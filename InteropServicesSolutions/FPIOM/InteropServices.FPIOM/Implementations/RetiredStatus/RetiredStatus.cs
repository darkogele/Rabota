using InteropServices.FPIOM.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InteropServices.FPIOM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class RetiredStatus: IRetiredStatus
    {
        public string GetRetiredStatus(string EMBG)
        {
            var EMBGTemp = new String(EMBG.Where(x => Char.IsDigit(x)).ToArray());

            if (EMBGTemp.Length != 13)
                throw new System.ArgumentException("Погрешен ЕМБГ:", EMBG);
            string output = "";
            var client = new RetiredInsuredData.WSIO113PortTypeClient();
            var response = client.penzioner1s(EMBG);
            var stringReader = new StringReader(response);
            var serializer = new XmlSerializer(typeof(DataForRetiredDTO));
            var retiredDto = serializer.Deserialize(stringReader) as DataForRetiredDTO;
            if (retiredDto.PensionStatus == "1")
                output = "ПЕНЗИОНЕР";
            else
                output = "НЕ Е ПЕНЗИОНЕР";
            return output;
        }
    }
}
