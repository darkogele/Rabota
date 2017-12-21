using System.Configuration;
using System.ServiceModel;
using Contracts.Interfaces.IDataForUnpaidContributions;

namespace Implementations.Implementations.DataForUnpaidContributions
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForUnpaidContributions : IDataForUnpaidContributions
    {
        string pathAVRM = ConfigurationSettings.AppSettings.Get("PathAvrmNP");
        string pathFZOM = ConfigurationSettings.AppSettings.Get("PathFzomNP");
        string pathFPIOM = ConfigurationSettings.AppSettings.Get("PathFpiomNP");
        public byte[] GetNP_AVRM(string date)
        {
            return System.IO.File.ReadAllBytes(pathAVRM + "AVRM_" + date + "_NP.doc.gpg");
        }
        public byte[] GetNP_FZOM(string date)
        {
            return System.IO.File.ReadAllBytes(pathFZOM + "FZOM_" + date + "_NP.doc.gpg");
        }
        public byte[] GetNP_FPIOM(string date)
        {
            return System.IO.File.ReadAllBytes(pathFPIOM + "FPIOM_" + date + "_NP.doc.gpg");
        }
    }
}
