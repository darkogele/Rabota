using System.Xml.Serialization;
using Interop.CC.SOAPstructure;

namespace Interop.CC.SOAPstructure.XmlSoap
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SOAP
    {
        [XmlElement(ElementName = "Header")]
        public Header Header;

        [XmlElement(ElementName = "Body")]
        public Body Body;
    }

    public class Header
    {
        [XmlElement(ElementName = "MIMHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MIMHeader MIMHeader;

        [XmlElement(ElementName = "MIMadditionalHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MIMadditionalHeader MIMadditionalHeader;

        [XmlElement(ElementName = "CryptoHeader", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public CryptoHeader CryptoHeader;

    }

    public class Body
    {
        [XmlElement(ElementName = "MIMBody", Namespace = "http://mioa.gov.mk/interop/mim/v1")]
        public MIMbody MIMbody;
    }

}

