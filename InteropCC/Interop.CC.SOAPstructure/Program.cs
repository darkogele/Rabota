using System;
using System.Xml.Serialization;
using Interop.CC.SOAPstructure.XmlSoap;
using Interop.CC.SOAPstructure.XmlSoapFault;

namespace Interop.CC.SOAPstructure
{
    class Program
    {
        static void Main(string[] args)
        {
            // SOAP instances


            SOAP soap = new SOAP()
            {
                Header = new Header()
                {
                    MIMHeader = new MIMHeader() {},
                    MIMadditionalHeader = new MIMadditionalHeader() {},
                    CryptoHeader = new CryptoHeader() {}
                },
                Body = new Body()
                {
                    MIMbody = new MIMbody() {}
                }
            };

            // SOAP NameSpace instance & serialization

            XmlSerializerNamespaces soapNS = new XmlSerializerNamespaces();

            soapNS.Add("mioa", "http://mioa.gov.mk/interop/mim/v1");
            soapNS.Add("xs", "http://www.w3.org/2001/XMLSchema");
            soapNS.Add("soap", "http://www.w3.org/2003/05/soap-envelope");

            // SOAP msg Serialization

            XmlSerializer xserSoap = new XmlSerializer(typeof(SOAP));

            xserSoap.Serialize(Console.Out, soap, soapNS);


            Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine);

            // FaultSOAP instances

            SOAPFault soapFault = new SOAPFault
            {
                Body = new FaultBody
                {
                    Fault = new Fault
                    {
                        Code = new Code { SubCode = new SubCode { } },
                        Details = new Detail {},
                        Reason = new Reason { Text = new Text { } }
                    }
                }
            };

            // FaultSOAP NameSpace instance & serialization

            XmlSerializerNamespaces soapFaultNS = new XmlSerializerNamespaces();

            soapFaultNS.Add("env", "http://www.w3.org/2003/05/soap-envelope");
            soapFaultNS.Add("m", "http://www.example.org/timeouts");
            soapFaultNS.Add("xml", "http://www.w3.org/XML/1998/namespace");

            // FaultSOAP msg Serialization

            XmlSerializer xserSoapFault = new XmlSerializer(typeof(SOAPFault));
            xserSoapFault.Serialize(Console.Out, soapFault, soapFaultNS);
        }

    }
}
