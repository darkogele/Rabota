using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Configuration;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IInsuredStatus;
using Oracle.ManagedDataAccess.Client;

namespace Implementations.Implementations.InsuredStatus
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class InsuredStatus : IInsuredStatus
    {
        OracleConnection _connection = null;
        //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";
        string _sql = WebConfigurationManager.AppSettings["InsuredStatusSql"];
        string oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
        string oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
        string datasource = WebConfigurationManager.AppSettings["InsuredStatusDataSource"];
        bool _hasRows = false;
        public string GetInsuredStatus(string embg)
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

            string _connectionString = "User Id=" + oracleUser + ";Password=" + oraclePass + datasource;

            try
            {
                string output = "не е пронајден матичниот број";
                _connection = new OracleConnection(_connectionString);

                try
                {
                    _connection.Open();
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
                var command = new OracleCommand(_sql, _connection);
                command.Parameters.Add(new OracleParameter("p_embg", embg));
                var reader = command.ExecuteReader();
                _hasRows = reader.HasRows; 

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
                else if (!_hasRows)
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот на институцијата врати порака:",
                        ErrorDetails = "Податоци за внесениот ЕМБГ: " + embg + " не постојат."
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }
                return output;
            }
            catch (FaultException<InteropFault> ex)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
