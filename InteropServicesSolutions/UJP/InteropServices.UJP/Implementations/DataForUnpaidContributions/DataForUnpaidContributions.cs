using InteropServices.UJP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForUnpaidContributions:IDataForUnpaidContributions
    {
        string pathAVRM = "C:\\INTEROPERABILNOST\\AVRM\\NP\\";
        string pathFZOM = "C:\\INTEROPERABILNOST\\FZOM\\NP\\";
        string pathFPIOM = "C:\\INTEROPERABILNOST\\FPIOM\\NP\\";
        public byte[] GetNP_AVRM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathAVRM + "AVRM_" + Date + "_NP.doc.gpg");
        }
        public byte[] GetNP_FZOM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathFZOM + "FZOM_" + Date + "_NP.doc.gpg");
        }
        public byte[] GetNP_FPIOM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathFPIOM + "FPIOM_" + Date + "_NP.doc.gpg");
        }
    }
}
