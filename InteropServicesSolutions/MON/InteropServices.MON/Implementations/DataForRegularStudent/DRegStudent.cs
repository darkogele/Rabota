using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.MON.Interfaces;
using System.Xml;
using System.Xml.Serialization;

namespace InteropServices.MON.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DRegStudent : IDRegStudent
    {
        public string GetStuD(string EMBG)
        {
            return Helper.GetData(EMBG);
        }
    }
}
