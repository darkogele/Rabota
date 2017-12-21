using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Interop.CC.Portal.API.Helpers
{
    public class WsdlParsingHelper
    {
        public static List<string> WsdlGetMethodNames(string wsdl)
        {
            var names = new List<string>();
            XDocument doc = XDocument.Parse(wsdl);
            XNamespace Wsdl = "http://schemas.xmlsoap.org/wsdl/";
            XNamespace soap12 = "http://schemas.xmlsoap.org/wsdl/soap12/";
            if (doc.Root != null)
            {
                var bindings = doc.Descendants(Wsdl + "binding");
                var operations = new List<XElement>();
                foreach (XElement binding in bindings)
                {
                    var bindingDescendants = binding.Descendants(soap12 + "operation").ToList();
                    if (bindingDescendants.Any())
                    {
                        operations.AddRange(bindingDescendants);
                    }

                }
                if (operations.Any())
                {
                    foreach (XElement operation in operations)
                    {
                        names.Add(operation.Attribute("soapAction").Value);

                    }
                }
                else
                    throw new Exception("Soap12 soapAction not present!!!");
            }
            return names;
        }
    }
}