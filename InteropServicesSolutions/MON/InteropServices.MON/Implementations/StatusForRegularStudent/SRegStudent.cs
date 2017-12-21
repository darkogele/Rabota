using InteropServices.MON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace InteropServices.MON.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class SRegStudent: ISRegStudent
    {
        public string GetStuS(string EMBG)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Helper.GetData(EMBG));
            var status = doc.GetElementsByTagName("schoolClassStatus");
            if (status.Count != 0)
                return status[0].InnerText;
            else
                return "Не е пронајден матичниот број";
        }
    }
}
