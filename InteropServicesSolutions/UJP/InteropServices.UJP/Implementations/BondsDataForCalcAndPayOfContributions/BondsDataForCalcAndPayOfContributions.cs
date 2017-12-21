using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.UJP.Interfaces;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class BondsDataForCalcAndPayOfContributions : IBondsDataForCalcAndPayOfContributions
    {
        string pathAVRM = "C:\\INTEROPERABILNOST\\AVRM\\OU\\";
        string pathFZOM = "C:\\INTEROPERABILNOST\\FZOM\\OU\\";
        string pathFPIOM = "C:\\INTEROPERABILNOST\\FPIOM\\OU\\";
        public byte[] GetOU_AVRM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathAVRM + "AVRM_" + Date + "_OU.txt.gpg");
        }
        public byte[] GetOU_FZOM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathFZOM + "FZOM_" + Date + "_OU.txt.gpg");
        }
        public byte[] GetOU_FPIOM(string Date)
        {
            return System.IO.File.ReadAllBytes(pathFPIOM + "FPIOM_" + Date + "_OU.txt.gpg");
        }
    }
}
