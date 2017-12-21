using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Configuration;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IInsuredStatus;
using Helpers;
using Oracle.ManagedDataAccess.Client;

namespace ImplementationsTest.Implementations.InsuredStatus
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class InsuredStatusTest : IInsuredStatus
    {
        OracleConnection _connection = null;
        //string _connectionString = "User Id=INTEROPERAB;Password=interdvaoperabilnost;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.10.111)(PORT=1521))(CONNECT_DATA=(SID=DB1)));";
        readonly string _sql = WebConfigurationManager.AppSettings["InsuredStatusSql"];
        readonly string _oracleUser = WebConfigurationManager.AppSettings["OracleUser"];
        readonly string _oraclePass = WebConfigurationManager.AppSettings["OraclePass"];
        readonly string _datasource = WebConfigurationManager.AppSettings["InsuredStatusDataSource"];
        bool _hasRows;
        public string GetInsuredStatus(string embg)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(embg))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот ЕМБГ е празен. Вредноста внесена за ЕМБГ треба да содржи 13 цифри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Внесениот ЕМБГ е невалиден. Вредноста внесена за ЕМБГ не треба да содржи карактери/симболи!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
                if (embgTemp.Length != 13)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Внесениот ЕМБГ е невалиден. Вредноста внесена за ЕМБГ треба да содржи 13 цифри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                string connectionString = "User Id=" + _oracleUser + ";Password=" + _oraclePass + _datasource;
                string output = string.Empty;
                _connection = new OracleConnection(connectionString);
                _connection.Open();

                var command = new OracleCommand(_sql, _connection);
                command.Parameters.Add(new OracleParameter("p_embg", embg));
                var reader = command.ExecuteReader();
                _hasRows = reader.HasRows;

                if (!_hasRows)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Податоци за внесениот ЕМБГ " + embg + " не постојат!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                while (reader.Read())
                {
                    output = reader["DO_DATUM"].ToString();
                }
                _connection.Close();
                output = output == "00.00.0000" ? "ВРАБОТЕН" : "НЕВРАБОТЕН";

                #endregion

                return output;
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (OracleException)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до базата на ФПИОМ не може да се воспостави!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (Exception ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }
    }
}
