using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Configuration;
using KIBS.kibs;
using KIBS.KIBSProd;
using Org.BouncyCastle.Cms;

namespace KIBS
{
    public class KIBS
    {
        public KIBS()
        {
            
        }
        public static KIBSResponse GenerateTimeStamp(string mimMessage)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(mimMessage);
            SHA1Managed sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(bytData);

            var oWS = new wsTSATest();
            oWS.Url = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSUrl", "");
            var kibsCertificationPath = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSCertificationPath", "");  //WebConfigurationManager.AppSettings["KIBSCertificationPath"];
            var kibsCertificationPassword = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSCertificationPassword", "");  //WebConfigurationManager.AppSettings["KIBSCertificationPassword"];
            var cer = new X509Certificate2(kibsCertificationPath, kibsCertificationPassword);
            oWS.ClientCertificates.Add(cer);
            var resp = oWS.funGenerateTS_Bytes(hash);
            oWS.Dispose();
            
            var convert = Convert.ToBase64String(resp.bytTSToken);
            var token = new Org.BouncyCastle.Tsp.TimeStampToken(new CmsSignedData(resp.bytTSToken));
            var datetimeTS = token.TimeStampInfo.GenTime;
            string strFailureInfo = resp.strFailureInfo;
            var response = new KIBSResponse();
            if (strFailureInfo != "")
            {
                response.IsSuccessful = false;
            }

            
            response.Hash = convert;
            response.TimeStamp = datetimeTS;
            return response;
        }

        public static KIBSResponse GenerateTimeStampProduction(string mimMessage)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(mimMessage);
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(bytData);

            var clientProduction = new wsTSA();
            clientProduction.Url = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSUrlProd", "");
            var kibsCertificationPath = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSCertificationPathProd", ""); //WebConfigurationManager.AppSettings["KIBSCertificationPath"];
            var kibsCertificationPassword = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\MIM", "KIBSCertificationPasswordProd", ""); //WebConfigurationManager.AppSettings["KIBSCertificationPassword"];
            var cer = new X509Certificate2(kibsCertificationPath, kibsCertificationPassword);
            clientProduction.ClientCertificates.Add(cer);
            var responseFromKibs = clientProduction.funGenerateTS_Bytes(hash);
            clientProduction.Dispose();

            var hashFromKibs = Convert.ToBase64String(responseFromKibs.bytTSToken);
            var token = new Org.BouncyCastle.Tsp.TimeStampToken(new CmsSignedData(responseFromKibs.bytTSToken));
            var dateTimeFromKibs = token.TimeStampInfo.GenTime;
            string strFailureInfo = responseFromKibs.strFailureInfo;
            var response = new KIBSResponse();
            if (strFailureInfo != "")
            {
                response.IsSuccessful = false;
            }
            response.Hash = hashFromKibs;
            response.TimeStamp = dateTimeFromKibs;
            return response;
        }
    }
}
