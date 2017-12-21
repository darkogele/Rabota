using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReadWsdlFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string decryptedRequestBodyWrappedInSoapEnvelope;
            string contentTypeToUse;
            string url;
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo != null)
            {
                var file = directoryInfo.FullName + "/File/PropertyListTest.wsdl";
                if (File.Exists(file))
                {
                    var fileText = File.ReadAllText(file);
                    XDocument doc = XDocument.Parse(fileText);
                    if (doc.Root != null)
                    {
                        XNamespace shema = "http://www.w3.org/2001/XMLSchema";
                        XElement[] messageElements = doc.Descendants(shema + "element").ToArray();
                        var elementWithInputParams = messageElements.FirstOrDefault();
                        var methodName = elementWithInputParams.Attribute("name").Value;
                        {
                            XElement[] sequences = elementWithInputParams.Descendants(shema + "sequence").Descendants().ToArray();
                            var builder = new StringBuilder();
                            builder.Append("<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>");
                            builder.Append("<" + methodName + " xmlns=\"http://interop.org/\">");
                            foreach (var sequence in sequences)
                            {

                                builder.Append("<" + sequence.Attribute("name").Value + ">");
                                builder.Append("1");
                                builder.Append("<" + sequence.Attribute("name").Value + ">");
                            }
                            builder.Append("<" + methodName + ">");
                            builder.Append("</s:Body></s:Envelope>");
                            decryptedRequestBodyWrappedInSoapEnvelope = builder.ToString();
                        }

                        XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
                        XElement[] portElements =
                            doc.Descendants(wsdl + "binding").Descendants(wsdl + "operation").Descendants().ToArray();
                        var elementWithAction = portElements.FirstOrDefault();

                        contentTypeToUse = elementWithAction.Attribute("soapAction").Value;

                        XElement[] serviceElements =
                            doc.Descendants(wsdl + "service").Descendants(wsdl + "port").Descendants().ToArray();
                        var elementWithAddress = serviceElements.FirstOrDefault();

                        url = elementWithAddress.Attribute("location").Value;

                    }
                    

                    Console.WriteLine(fileText);
                    Console.ReadLine();
                }
            }
        }
    }
}
