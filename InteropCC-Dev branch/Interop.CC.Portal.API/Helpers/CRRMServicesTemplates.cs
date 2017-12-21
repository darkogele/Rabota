using System.Xml;
using System.Xml.Linq;

namespace Interop.CC.Portal.API.Helpers
{
    public class CRRMServicesTemplates
    {
        public static XmlDocument CreateCRMCURM_XMLTest(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LEOSSCurrentView"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRM_XML(string edb, string productName)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", productName),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRM_XMLTest(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LEOSSCurrentView"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRMAKN_XMLTest(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "OSSLECView"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmlDoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmlDoc.Load(xmlReader);
            }
            return xmlDoc;
        }
        public static XmlDocument CreateCRMAKN_XML(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LECViewForAKN"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmlDoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmlDoc.Load(xmlReader);
            }
            return xmlDoc;
        }
        public static XmlDocument CreateCRMUJP_XMLTest(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LECViewTest"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVCourtProc"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRMUJP_XML(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LECViewForUJP"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVCourtProc"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRMListOfChanges_XML(string date)
        {
            var xdoc = new XDocument(
              new XElement("CrmRequest",
               new XAttribute("ProductName", "CU1"),
               new XElement("Parameters", new XAttribute("TemplateName", "CU_LEIDListChanges"),
                   new XElement("Parameter", date, new XAttribute("Name", "@ListType"))),
               new XElement("Parameters", new XAttribute("TemplateName", "CU_LEIDListChanges"),
                   new XElement("Parameter", date, new XAttribute("Name", "@DateFrom")))
              )
          );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument CreateCRMCURM_XML(string edb)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "CU1"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CU11"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CU12"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CU13"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
    }
}