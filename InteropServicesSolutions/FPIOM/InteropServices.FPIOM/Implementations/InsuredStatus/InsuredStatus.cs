using System;
using System.IO;
using System.Linq;
using InteropServices.FPIOM.Interfaces;
using System.ServiceModel;
using Oracle.ManagedDataAccess.Client;
using System.Web.Configuration;

namespace InteropServices.FPIOM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class InsuredStatus : IInsuredStatus
    {
        OracleConnection _connection = null;
        //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";
        string _sql = "select osig_obv.reg_br,converttoutf(obvrznici.naziv) naziv,obvrznici.danocen_broj, obvrznici.maticen_broj, od_datum, nvl(to_char(do_datum,'dd.mm.yyyy'),'00.00.0000') do_datum,casovi_nedelno from osig_obv,obvrznici where embg = :p_embg and obvrznici.reg_br=osig_obv.reg_br order by od_datum";
        public string GetInsuredStatus(string EMBG)
        {
            var oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
            var oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
            string _connectionString = "User Id="+oracleUser+";Password="+oraclePass+";Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";

            try
            {
                var EMBGTemp = new String(EMBG.Where(x => Char.IsDigit(x)).ToArray());

                if (EMBGTemp.Length != 13)
                    throw new System.ArgumentException("Погрешен ЕМБГ:", EMBG);

                string output = "не е пронајден матичниот број";
                _connection = new OracleConnection(_connectionString);


                _connection.Open();
                var command = new OracleCommand(_sql, _connection);
                command.Parameters.Add(new OracleParameter("p_embg", EMBG));
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    output = reader["DO_DATUM"].ToString();
                }
                _connection.Close();
                if (output == "00.00.0000")
                {
                    output = "ВРАБОТЕН";
                }
                else if (output != "не е пронајден матичниот број")
                {
                    output = "НЕВРАБОТЕН";
                }
                return output;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
