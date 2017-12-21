using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.UJP.Interfaces;
using System.IO;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForPaidContributionsByEmployeeAndPayer : IDataForPaidContributionsByEmployeeAndPayer
    {
        string pathAVRM = "C:\\INTEROPERABILNOST\\AVRM\\PP\\";
        string pathFZOM = "C:\\INTEROPERABILNOST\\FZOM\\PP\\";
        string pathFPIOM = "C:\\INTEROPERABILNOST\\FPIOM\\PP\\";
        public byte[] GetPP_AVRM(string Date, int AditionalFile)
        {
            byte[] output = null;
            try
            {
                if(AditionalFile == 0)
                output =  System.IO.File.ReadAllBytes(pathAVRM + "AVRM_" + Date + "_PP.zip.gpg");
                else
                    output = System.IO.File.ReadAllBytes(pathAVRM + "AVRM_" + Date + "_PP_D"+AditionalFile+".zip.gpg");
            }
            catch (Exception e)
            {}
            return output;
        }
        public byte[] GetPP_FZOM(string Date, int AditionalFile)
        {
            byte[] output = null;
            try
            {
                if (AditionalFile == 0)
                    output = System.IO.File.ReadAllBytes(pathFZOM + "FZOM_" + Date + "_PP.zip.gpg");
                else
                    output = System.IO.File.ReadAllBytes(pathFZOM + "FZOM_" + Date + "_PP_D" + AditionalFile + ".zip.gpg");
            }
            catch (Exception e)
            { }
            return output;
        }
        public byte[] GetPP_FPIOM(string Date, int AditionalFile)
        {
            byte[] output = null;
            try
            {
                if (AditionalFile == 0)
                    output = System.IO.File.ReadAllBytes(pathFPIOM + "FPIOM_" + Date + "_PP.zip.gpg");
                else
                    output = System.IO.File.ReadAllBytes(pathFPIOM + "FPIOM_" + Date + "_PP_D" + AditionalFile + ".zip.gpg");
            }
            catch (Exception e)
            { }
            return output;
        }
    }
}
