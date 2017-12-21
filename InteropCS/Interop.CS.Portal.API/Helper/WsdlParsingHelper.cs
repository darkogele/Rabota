using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Interop.CS.Portal.API
{
    public class WsdlParsingHelper
    {
        public static List<string> WsdlGetMethodNames(string WSDL)
        {
            List<string> names = new List<string>();
            XDocument doc = XDocument.Parse(WSDL);
            XNamespace Wsdl = "http://schemas.xmlsoap.org/wsdl/";
            XNamespace soap12 = "http://schemas.xmlsoap.org/wsdl/soap12/";
            if (doc.Root != null)
            {
                var bindings = doc.Descendants(Wsdl + "binding");
                List<XElement> operations = new List<XElement>();
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


                //foreach (var element in doc.Root.DescendantsAndSelf())
                //{
                //    element.Name = element.Name.LocalName;
                //    element.ReplaceAttributes(element.Attributes()
                //    .Where(x => !x.IsNamespaceDeclaration)
                //    .Select(x => new XAttribute(x.Name.LocalName, x.Value)));
                //}
                //var operations = doc.Descendants("portType").FirstOrDefault().Descendants("operation");
                //foreach (XElement value in operations)
                //{
                //    names.Add(value.Attribute("name").Value);
                //}
            }
            return names;
        }
    }
}