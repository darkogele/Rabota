using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web.Configuration;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IYearsOfWorkExperience;
using Contracts.Models.YearsOfWorkExperience;
using Implementations.Helpers.YearsOfWorkExperience;
using Oracle.ManagedDataAccess.Client;

namespace Implementations.Implementations.YearsOfWorkExperience
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class YearsOfWorkExperience : IYearsOfWorkExperience
    {
        OracleConnection _connection = null;
        //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";
        List<string> _sqls = new List<string>
        {
            "select distinct converttoutf(osigurenici.prezime) prezime, converttoutf(osigurenici.ime) ime, decode(osigurenici.pol,'Z','женски','M','машки') pol, to_char(osigurenici.datum_raganje,'dd.mm.yyyy') datum_raganje from osigurenici where embg = :p_embg",
            "select osig_obv.reg_br,converttoutf(obvrznici.naziv) naziv,obvrznici.danocen_broj, obvrznici.maticen_broj, od_datum, nvl(to_char(do_datum,'dd.mm.yyyy'),'00.00.0000') do_datum,casovi_nedelno from osig_obv,obvrznici where embg = :p_embg and obvrznici.reg_br=osig_obv.reg_br order by od_datum",
            "select 'ПС', to_char(perod,'yyyy') godina, NAZIV_DRZAVA drzava ,perod period_od, perdo period_do,trast, vstaz, stepen,months_between(PERDO ,PEROD) traenje from ps_staz,s_beneficii,DRZAVA where embg=:p_embg and vstaz=id_benef and nvl(vid_promena,0) !=1 and srpd=sifra_drzava order by godina, period_od",
            "SELECT decode(mvid,'41','M-4','42','M-4','43','M-4', '44','M-4', '45','M-4', '51','M-5','61','M-6','62','M-6','63','M-6','81','M-8','82','M-8','83','M-8', '84','M-8', '85','M-8','91','M-9','11','M-10','12','M-10','13','M-10') obrazec, GODINA, REGB, PEROD, PERDO, TRAST, CASLD, DINLD, decode(GBOL,'0000','',GBOL) god_bol, eftra,stepen, (case when perod <> '0000' and perdo<>'0000' and godina<>'0000' then months_between(TO_DATE(PERDO||GODINA,'ddmmyyyy') ,TO_DATE(PEROD||GODINA,'ddmmyyyy')) else null end) meseci FROM M456VK,s_beneficii WHERE EMBG=:P_EMBG and id_benef=decode(vstaz,'0000',nvl(pkg_p10.vid_staz(m_id),vstaz),vstaz) order by GODINA",
            "SELECT decode(mvid,'41','M-4','42','M-4','43','M-4', '44','M-4', '45','M-4', '51','M-5','61','M-6','62','M-6','63','M-6','81','M-8','82','M-8','83','M-8', '84','M-8', '85','M-8','91','M-9','11','M-10','12','M-10','13','M-10') obrazec, GODINA, REGB, PEROD, PERDO, TRAST, CASLD, DINLD, decode(GBOL,'0000','',GBOL) god_bol, eftra,stepen FROM M456VK_SLUZBA,s_beneficii WHERE EMBG=:P_EMBG and id_benef=decode(vstaz,'0000',nvl(pkg_p10.vid_staz(m_id),vstaz),vstaz) order by GODINA"
        };
        public byte[] GetYWExpReport(string embg)
        {
            var oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
            var oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
            byte[] _Buffer = null;
            string url = "http://172.17.10.24:7778/reports/rwservlet?destype=CACHE&desformat=PDF&server=repstest1&report=PREGLED_PODATOCI_MATICNA.rdf&userid=" + oracleUser + "/" + oraclePass + "@DB2_NOVA&P_EMBG=";
            using (WebClient client = new WebClient())
            {
                _Buffer = client.DownloadData(url + embg);
            }
            return _Buffer;
        }
        public YearsOfWorkExperienceOutput GetYWExpXML(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Параметарот ЕМБГ е празен. Вредноста внесена за ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Вредноста внесена за ЕМБГ содржи карактери/симболи."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Вредноста внесена за ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            try
            {
                OpenConnection();
            }
            catch (OracleException e)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Конекцијата до ФПИОМ сервисот не може да се воспостави."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            try
            {
                
                var output = new YearsOfWorkExperienceOutput();

                var readers = GetData(_connection, _sqls, embg);

                output.GeneralData = ObjectGeneratorHelper.GetGeneralData(readers[0]);

                if (output.GeneralData.Name == null)
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот на институцијата врати порака:",
                        ErrorDetails = "Податоци за внесениот ЕМБГ: " + embg + " не постојат."
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }
                output.InsuranceData = ObjectGeneratorHelper.GetInsuranseData(readers[1]);
                output.OldAndForeignExperience = ObjectGeneratorHelper.GetOldAndForeignExperience(readers[2]);
                output.M4 = ObjectGeneratorHelper.GetM4(readers[3]);
                output.InvalidM4 = ObjectGeneratorHelper.GetInvalidM4(readers[4]);
                CloseConnection();
                return output;
            }
            catch (FaultException<InteropFault> ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        void OpenConnection()
        {
            var oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
            var oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
            var yearsOfWorkExpDataSource = WebConfigurationManager.AppSettings["YearsOfWorkExpDataSource"];
            string _connectionString = "User Id=" + oracleUser + ";Password=" + oraclePass + yearsOfWorkExpDataSource;
            //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";

            _connection = new OracleConnection(_connectionString);
            _connection.Open();
        }
        void CloseConnection()
        {
            _connection.Close();
        }
        List<OracleDataReader> GetData(OracleConnection connection, List<string> sqls, string embg)
        {
            var output = new List<OracleDataReader>();
            for (int i = 0; i < sqls.Count; i++)
            {
                var command = new OracleCommand(sqls[i], connection);
                command.Parameters.Add(new OracleParameter("p_embg", embg));
                var reader = command.ExecuteReader();
                output.Add(reader);
            }
            return output;
        }
    }
}
