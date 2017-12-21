using System;
using System.Configuration;

namespace Interop.CC.Models.DTO.Institutions
{
    public class BasicPrintInfoDTO
    {
        public DateTime ServiceCalledTime
        {
            get { return DateTime.Now; }
        }
        public string ServiceName { get; set; }
        public string ProviderName { get; set; }
        public string ConsumerName
        {
            get { return ConfigurationManager.AppSettings["InstitutionName"]; }
        }

        public void FillBasicPrintInfo(string serviceName, string providerName)
        {
            ServiceName = serviceName;
            ProviderName = providerName;
        }
    }
}
