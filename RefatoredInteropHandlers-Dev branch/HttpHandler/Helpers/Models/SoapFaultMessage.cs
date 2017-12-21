using System.Xml.Serialization;

namespace Helpers.Models
{

    // Опис: Структура на SOAPFault порака
    [XmlRoot("Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapFaultMessage
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

        [XmlElement(ElementName = "Detail")]
        public Detail Detail;

    }

    public class Code
    {
        [XmlElement(ElementName = "Value")]
        public string value = "env:Sender";

        [XmlElement(ElementName = "Subcode")]
        public Subcode Subcode;
    }

    public class Reason
    {
        [XmlElement(ElementName = "Text")]
        public Text Text;
    }

    public class Subcode
    {
        [XmlElement(ElementName = "Value")]
        public string value = "m:MessageTimeout";
    }


    public class Text
    {

        [XmlText]
        public string value = "Sender Timeout";

        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string name = "en";
    }

    public class Detail
    {
        [XmlElement(ElementName = "MaxTime", Namespace = "http://www.example.org/timeouts")]
        public string maxTime = "P5M";
    }

}

