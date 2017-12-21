using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;
using Interop.CC.CrossCutting.Logging;

namespace Interop.CC.MetaService.Helpers
{
    public static class XmlParseHelper
    {

        // Опис: Методот проверува дали рутот на XDocument документот е null објект
        // Влезни параметри: Document document, ILogger logger
        // Излезни параметри: XElement model
        public static XElement Check(this XDocument document, ILogger logger)
        {
            XElement checkedRoot;
            var root = document.Root;
            if (root != null)
                checkedRoot = root;
            else
            {
                logger.Error("XDocument root is null.", new Exception("XDocument root is null."));
                throw new FaultException("XDocument root is null.");
            }
            return checkedRoot;
        }

        // Опис: Методот проверува дали елементите во кои е вгнезден XElement имаат елементи
        // Влезни параметри: XElement element, XName name, ILogger logger
        // Излезни параметри: IEnumerable<XElement> листа
        public static IEnumerable<XElement> CheckDescendants(this XElement element, XName name, ILogger logger)
        {
            if (!element.Descendants(name).Any())
            {
                logger.Error("Descendants collection is empty. Descendants name = " + name.LocalName, new Exception("Descendants collection is empty."));
            }
            return element.Descendants(name);
        }

        // Опис: Методот проверува дали атрибутот со име за атрибут не е null објект
        // Влезни параметри: XElement element, XName attributeName, ILogger logger
        // Излезни параметри: XAttribute модел
        public static XAttribute CheckAttribute(this XElement element, XName attributeName, ILogger logger)
        {
            try
            {
                var attribute = element.Attribute(attributeName).Value;
                return element.Attribute(attributeName);
            }
            catch (Exception e)
            {
                logger.Error("Attribute with name = " + attributeName.LocalName + " is null.", e);
                throw new FaultException("Attribute with name = " + attributeName.LocalName + " is null.");
            }
        }

        // Опис: Методот проверува дали XElement е null објект
        // Влезни параметри: XElement element, XName name, ILogger logger
        // Излезни параметри: XElement модел
        public static XElement CheckElement(this XElement element, XName name, ILogger logger)
        {
            try
            {
                var el = element.Element(name).Value;
                return element.Element(name);
            }
            catch (Exception e)
            {
                logger.Error("Element with name = " + name.LocalName + " is null", e);
                throw new FaultException("Element with name = " + name.LocalName + " is null");
            }
        }

        // Опис: Методот проверува дали XElement од XDocument е null
        // Влезни параметри: XDocument document, string name, ILogger logger
        // Излезни параметри: XElement модел
        public static XElement CheckElement(this XDocument document, string name, ILogger logger)
        {
            try
            {
                var el = document.Element(name).Value;
                return document.Element(name);
            }
            catch (Exception e)
            {
                logger.Error("Element with name = " + name + " is null", e);
                throw new FaultException("Element with name = " + name + " is null");
            }
        }

        // Опис: Методот проверува дали постојат елементи во XElement колекцијата
        // Влезни параметри: XElement element, XName name, ILogger logger
        // Излезни параметри: IEnumerable<XElement> листа
        public static IEnumerable<XElement> CheckElements(this XElement element, XName name, ILogger logger)
        {
            if (!element.Elements(name).Any())
            {
                logger.Error("No elements found in XElement collection with name = " + name.LocalName, new Exception("No elements found in XElement collection with name = " + name.LocalName));
                throw new FaultException("No elements found in XElement collection with name = " + name.LocalName);
            }
            return element.Elements(name);
        }
    }
}