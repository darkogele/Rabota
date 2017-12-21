using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.FPIOM.Interfaces;
using System.IO;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Web.Configuration;


namespace InteropServices.FPIOM.Implementations
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
        public byte[] GetYWExpReport(string EMBG)
        {
            var oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
            var oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
            byte[] _Buffer = null;
            string url = "http://172.17.10.24:7778/reports/rwservlet?destype=CACHE&desformat=PDF&server=repstest1&report=PREGLED_PODATOCI_MATICNA.rdf&userid="+oracleUser+"/"+oraclePass+"@DB2_NOVA&P_EMBG=";
            using (WebClient client = new WebClient())
            {
                _Buffer = client.DownloadData(url+EMBG);
            }
            return _Buffer;
        }
        public YearsOfWorkExperienceOutput GetYWExpXML(string EMBG)
        {
            try
            {
                OpenConnection();
                YearsOfWorkExperienceOutput output = new YearsOfWorkExperienceOutput();
                List<OracleDataReader> readers = GetData(_connection, _sqls, EMBG);
                output.GeneralData = ObjectGeneratorHelper.GetGeneralData(readers[0]);
                if (output.GeneralData.Name == null)
                    throw new Exception("Нема податоци за дадениот матичен број");
                output.InsuranceData = ObjectGeneratorHelper.GetInsuranseData(readers[1]);
                output.OldAndForeignExperience = ObjectGeneratorHelper.GetOldAndForeignExperience(readers[2]);
                output.M4 = ObjectGeneratorHelper.GetM4(readers[3]);
                output.InvalidM4 = ObjectGeneratorHelper.GetInvalidM4(readers[4]);
                CloseConnection();
                return output;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }
        void OpenConnection()
        {
            var oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
            var oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
            string _connectionString = "User Id=" + oracleUser + ";Password=" + oraclePass + ";Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";
            //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";

            _connection = new OracleConnection(_connectionString);
            _connection.Open();
        }
        void CloseConnection()
        {
            _connection.Close();
        }
        List<OracleDataReader> GetData(OracleConnection connection, List<string> sqls, string EMBG)
        {
            List<OracleDataReader> output = new List<OracleDataReader>();
            for (int i = 0; i < sqls.Count; i++)
            {
                OracleCommand command = new OracleCommand(sqls[i], connection);
                command.Parameters.Add(new OracleParameter("p_embg", EMBG));
                OracleDataReader reader = command.ExecuteReader();
                output.Add(reader);
            }
            return output;
        }
    }
}
