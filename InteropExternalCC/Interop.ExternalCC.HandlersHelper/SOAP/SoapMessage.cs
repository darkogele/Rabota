using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Interop.ExternalCC.HandlersHelper.Model;

namespace Interop.ExternalCC.HandlersHelper.SOAP
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapMessage
    {
        [XmlElement(ElementName = "Header")]
        public Header Header;

        [XmlElement(ElementName = "Body")]
        public Body Body;
    }

    public class Header
    {
        [XmlElement(ElementName = "MIMHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MimHeader MimHeader;

        [XmlElement(ElementName = "MIMadditionalHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MimAdditionalHeader MimAdditionalHeader;

        [XmlElement(ElementName = "CryptoHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public CryptoHeader CryptoHeader;

    }

    public class Body
    {
        [XmlElement(ElementName = "MIMBody", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MimBody MimBody;
    }
}
