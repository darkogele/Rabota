using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Interop.CC.SOAPstructure.XmlSoapFault
{
    [XmlRoot("Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SOAPFault
    {
        [XmlElement(ElementName = "Body")]
        public FaultBody Body;
    }

    public class FaultBody
    {
         [XmlElement(ElementName = "Fault")]
        public Fault Fault;
    }

    public class Fault
    {
        [XmlElement(ElementName = "Code")]
        public Code Code;

        [XmlElement(ElementName = "Reason")]
        public Reason Reason;

        [XmlElement(ElementName = "Details")]
        public Detail Details;

    }

    public class Code
    {
        [XmlElement(ElementName = "Value")]
        public string value = "env:Sender";

        [XmlElement(ElementName = "SubCode")]
        public SubCode SubCode;
    }

    public class Reason
    {
        [XmlElement(ElementName = "Text")]
        public Text Text;
    }

    public class SubCode
    {
        [XmlElement(ElementName = "Value")]
        public string value = "m:MessageTimeout";
    }


    public class Text
    {

        [XmlText()]
        public string text = "Sender Timeout";

        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string name = "en";
    }

    public class Detail
    {
        [XmlElement(ElementName = "MaxTime", Namespace = "http://www.example.org/timeouts")]
        public string maxTime = "P5M";
    }

}

